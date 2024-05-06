using GateEntryExit_Umbraco.Dtos.Account;
using GateEntryExit_Umbraco.Dtos.Gate;
using GateEntryExit_Umbraco.Services.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace GateEntryExit_Umbraco.Services
{
    public class GateService : IGateService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _config;
        public GateService(IConfiguration config)
        {
            _client = new HttpClient();
            _config = config;
        }

        public async Task<GetAllGatesDto> GetAllAsync(int pageNumber = 1)
        {
            var accesstoken = await GetAccessTokenAsync();

            if(string.IsNullOrEmpty(accesstoken))
            {
                throw new Exception("Login process failed");
            }
            else
            {
                var maxResultCount = 5;
                var skipCount = (pageNumber - 1) * maxResultCount;
                var allgates = new GetAllGatesDto();

                var endpoint = "http://localhost:5058/api/gate/getAll";

                var postData = new GetAllDto { MaxResultCount = maxResultCount, SkipCount = skipCount, Sorting = "" };
                var jsonString = JsonConvert.SerializeObject(postData);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);
                HttpResponseMessage responseMessage = await _client.PostAsync(endpoint, content);

                if (responseMessage.IsSuccessStatusCode)
                {
                    string data = await responseMessage.Content.ReadAsStringAsync();
                    allgates = JsonConvert.DeserializeObject<GetAllGatesDto>(data);
                }

                return allgates;
            }
        }

        private async Task<string> GetAccessTokenAsync()
        {
            var userName = _config.GetValue<string>("Login:User");
            var password = _config.GetValue<string>("Login:Password");
            var authResponse = new AuthResponseDto();

            var endpoint = "http://localhost:5058/api/Account/login";

            var postData = new { Email = userName, Password = password };
            var jsonString = JsonConvert.SerializeObject(postData);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = await _client.PostAsync(endpoint, content);

            if (responseMessage.IsSuccessStatusCode)
            {
                string data = await responseMessage.Content.ReadAsStringAsync();
                authResponse = JsonConvert.DeserializeObject<AuthResponseDto>(data);
            }

            return authResponse.Token;
        }
    }
}
