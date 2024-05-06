using GateEntryExit_Umbraco.Dtos.Gate;
using GateEntryExit_Umbraco.Services.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace GateEntryExit_Umbraco.Services
{
    public class GateService : IGateService
    {
        private readonly HttpClient _client;
        public GateService()
        {
            _client = new HttpClient();
        }

        public async Task<GetAllGatesDto> GetAllAsync(int pageNumber = 1)
        {
            var maxResultCount = 5;
            var skipCount = (pageNumber - 1) * maxResultCount;
            var model = new GetAllGatesDto();
            var endpoint = "http://localhost:5058/api/gate/getAll";
            var postData = new GetAllDto { MaxResultCount = maxResultCount, SkipCount = skipCount, Sorting = "" };

            var jsonString = JsonConvert.SerializeObject(postData);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await _client.PostAsync(endpoint, content);

            if (responseMessage.IsSuccessStatusCode)
            {
                string data = await responseMessage.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<GetAllGatesDto>(data);
            }

            return model;
        }

        private async Task GetAccessToken()
        {

        }
    }
}
