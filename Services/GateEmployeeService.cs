using Azure.Core;
using GateEntryExit_Umbraco.Dtos.Gate;
using GateEntryExit_Umbraco.Dtos.GateEmployee;
using GateEntryExit_Umbraco.Services.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace GateEntryExit_Umbraco.Services
{
    public class GateEmployeeService : IGateEmployeeService
    {
        private readonly HttpClient _client;

        public GateEmployeeService()
        {
            _client = new HttpClient();
        }

        public async Task<AllGateEmployeeDto> GetAllAsync(int pageNumber = 0)
        {
            var maxResultCount = 5;
            var skipCount = pageNumber * maxResultCount;
            var allgateEmployee = new AllGateEmployeeDto();

            var endpoint = "http://localhost:8081/api/gateEmployee/getAll";

            var postData = new { page = pageNumber, size = maxResultCount };
            var jsonString = JsonConvert.SerializeObject(postData);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = await _client.PostAsync(endpoint, content);

            if (responseMessage.IsSuccessStatusCode)
            {
                string data = await responseMessage.Content.ReadAsStringAsync();
                allgateEmployee = JsonConvert.DeserializeObject<AllGateEmployeeDto>(data);
            }

            return allgateEmployee;
        }
    }
}
