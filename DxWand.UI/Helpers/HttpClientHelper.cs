using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DxWand.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace DxWand.UI.Helpers
{
    public class HttpClientHelper
    {
        public static async Task<R> GetAsync<R>(string resourceUrl, [FromServices] IApplicationService applicationService 
            , [FromServices] IConfiguration configuration)
        {
            var API_BASE_URL = configuration.GetSection("API_BASE_URL").Value;
            R response = default(R);
            using (var httpClient = new HttpClient())
            {
                var token = applicationService.GetToken();
                if (!string.IsNullOrWhiteSpace(token))
                {
                    httpClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", token);
                }

                using (var getResponse = await httpClient.GetAsync(API_BASE_URL + resourceUrl))
                {
                    if(getResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        applicationService.ClearToken();
                    }
                    string apiResponse = await getResponse.Content.ReadAsStringAsync();
                    response = JsonConvert.DeserializeObject<R>(apiResponse);
                }
            }
            return response;
        }

        public static async Task<R> PostAsync<T,R>(string resourceUrl, T param, [FromServices] IApplicationService applicationService
            , [FromServices] IConfiguration configuration)
        {
            var API_BASE_URL = configuration.GetSection("API_BASE_URL").Value;
            R response = default(R);
            using (var httpClient = new HttpClient())
            {
                StringContent content = null;
                if (param != null)
                    content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");

                var token = applicationService.GetToken();
                if (!string.IsNullOrWhiteSpace(token))
                {
                    httpClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", token);
                }

                using (var postResponse = await httpClient.PostAsync(API_BASE_URL + resourceUrl, content))
                {
                    if (postResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        applicationService.ClearToken();
                    }
                    string apiResponse = await postResponse.Content.ReadAsStringAsync();
                    response = JsonConvert.DeserializeObject<R>(apiResponse);
                }
            }
            return response;
        }
    }
}
