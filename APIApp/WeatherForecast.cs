using System.Text.Json.Serialization;

namespace APIApp
{
    public class WeatherForecast
    {
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }

        #region For Redis Testing

        [JsonPropertyName("number")]
        public int Number { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("startTime")]
        public DateTime StartTime { get; set; }

        [JsonPropertyName("endTime")]
        public DateTime EndTime { get; set; }

        [JsonPropertyName("isDayTime")]
        public bool IsDayTime { get; set; }

        [JsonPropertyName("temperature")]
        public int Temperature { get; set; }

        [JsonPropertyName("temperatureUnit")]
        public string? TemperatureUnit { get; set; }

        [JsonPropertyName("temperatureTrend")]
        public string? TemperatureTrend { get; set; }

        [JsonPropertyName("windSpeed")]
        public string? WindSpeed { get; set; }

        [JsonPropertyName("windDirection")]
        public string? WindDirection { get; set; }

        [JsonPropertyName("shortForecast")]
        public string? ShortForecast { get; set; }

        [JsonPropertyName("detailedForecast")]
        public string? DetailedForecast { get; set; }
        #endregion
    }

}
