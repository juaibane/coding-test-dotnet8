using CustomerApi.Domain.Entities;

namespace CustomerApi.Infrastructure.Persistence.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllAsync();
        Task ReplaceData(IEnumerable<Customer> customers);
    }
}
