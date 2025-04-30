using Simulator.Core.Interfaces;
using Simulator.Core.Models;
using System.Net.Http.Json;

namespace Simulator.Infrastructure.Services
{
    public class CustomerApiClient : ICustomerApiClient
    {
        private readonly HttpClient _httpClient;

        public CustomerApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> PostCustomersAsync(IEnumerable<CustomerDto> customers)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/customers", customers);
            return response;
        }

        public async Task<IEnumerable<CustomerDto>> GetCustomersAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<CustomerDto>>("/api/customers");
            return response ?? Enumerable.Empty<CustomerDto>();
        }
    }
}
