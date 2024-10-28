using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace APIApp.Controllers.RedisTesting
{
    [ApiController]
    [Route("api/[controller]")]
    public class RedisController : BaseController
    {
        private readonly HttpClient _client;

        public RedisController(HttpClient client, IConnectionMultiplexer muxer)
        {
            _client = client;
            _client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("weatherCachingApp", "1.0"));
            _redis = muxer.GetDatabase();
        }

        private readonly IDatabase _redis;


        [HttpGet("Redis-Test")]
        public async Task<ForecastResult> Get([FromQuery] double latitude, [FromQuery] double longitude)
        {
            string json;
            var watch = Stopwatch.StartNew();
            var keyName = $"forecast:{latitude},{longitude}";
            json = await _redis.StringGetAsync(keyName);
            if (string.IsNullOrEmpty(json))
            {
                json = await GetForecast(latitude, longitude);
                var setTask = _redis.StringSetAsync(keyName, json);
                var expireTask = _redis.KeyExpireAsync(keyName, TimeSpan.FromSeconds(3600));
                await Task.WhenAll(setTask, expireTask);
            }

            var forecast =
                JsonSerializer.Deserialize<IEnumerable<WeatherForecast>>(json);
            watch.Stop();
            var result = new ForecastResult(forecast, watch.ElapsedMilliseconds);

            return result;
        }

        //16.848080275784394, 96.18149365029845
        private async Task<string> GetForecast(double latitude, double longitude)
        {
            var pointsRequestQuery = $"https://api.weather.gov/points/{latitude},{longitude}"; //get the URI
            var result = await _client.GetFromJsonAsync<JsonObject>(pointsRequestQuery);
            var gridX = result["properties"]["gridX"].ToString();
            var gridY = result["properties"]["gridY"].ToString();
            var gridId = result["Properties"]["gridId"].ToString();
            var forecastRequestQuery = $"https://api.weather.gov/gridpoints/{gridId}/{gridX},{gridY}/forecast";
            var forecastResult = await _client.GetFromJsonAsync<JsonObject>(forecastRequestQuery);
            var periodsJson = forecastResult["properties"]["periods"].ToJsonString();
            return periodsJson;
        }
    }
}
