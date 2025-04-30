using Simulator.Core.Models;

namespace Simulator.Core.Interfaces
{
    public interface ICustomerGenerator
    {
        IEnumerable<CustomerDto> GenerateCustomers(int customersToGenerate, int startId);
    }
}
