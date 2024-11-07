using Newtonsoft.Json;
using Shared.Enum;
using System.Text;

namespace Shared.ApiService
{
    public class HttpClientService
    {
        private readonly HttpClient _httpClient;

        public HttpClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            httpClient.BaseAddress = new Uri("/");
        }

        public async Task<T> ExecuteAsync<T>(
            string endpoint, EnumHttpMethod httpMethod, object reqModel = null)
        {
            T model = default;
            HttpResponseMessage response = null;
            HttpContent content = null;

            if (reqModel != null)
            {
                content = new StringContent(JsonConvert.SerializeObject(reqModel), encoding: Encoding.UTF8, "application/json");
            }

            //switch (httpMethod)
            //{
            //	case EnumHttpMethod.POST:
            //		response = await _httpClient.PostAsync(endpoint, content);
            //		break;
            //	case EnumHttpMethod.PUT:
            //		response = await _httpClient.PutAsync(endpoint, content);
            //		break;
            //	case EnumHttpMethod.PATCH:
            //		response = await _httpClient.PatchAsync(endpoint, content);
            //		break;
            //	case EnumHttpMethod.DELETE:
            //		response = await _httpClient.DeleteAsync(endpoint);
            //		break;
            //	case EnumHttpMethod.GET:
            //	default:
            //		response = await _httpClient.GetAsync(endpoint);
            //		break;
            //}

            response = httpMethod switch
            {
                EnumHttpMethod.POST => await _httpClient.PostAsync(endpoint, content),
                EnumHttpMethod.PUT => await _httpClient.PutAsync(endpoint, content),
                EnumHttpMethod.PATCH => await _httpClient.PatchAsync(endpoint, content),
                EnumHttpMethod.DELETE => await _httpClient.DeleteAsync(endpoint),
                EnumHttpMethod.GET => await _httpClient.GetAsync(endpoint),
                _ => throw new Exception("Invalid http method")
            };

            var jsonStr = await response.Content.ReadAsStringAsync();
            model = JsonConvert.DeserializeObject<T>(jsonStr);

            return model;
        }
    }
}
