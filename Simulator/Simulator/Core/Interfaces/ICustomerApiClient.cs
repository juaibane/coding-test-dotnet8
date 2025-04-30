using Simulator.Core.Models;

namespace Simulator.Core.Interfaces
{
    public interface ICustomerApiClient
    {
        Task<HttpResponseMessage> PostCustomersAsync(IEnumerable<CustomerDto> customers);
        Task<IEnumerable<CustomerDto>> GetCustomersAsync();
    }
}
