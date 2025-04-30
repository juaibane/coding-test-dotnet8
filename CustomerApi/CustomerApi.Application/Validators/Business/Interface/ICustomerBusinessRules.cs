using CustomerApi.Application.Dtos;
using CustomerApi.Domain.Entities;

namespace CustomerApi.Application.Validators.Business.Interface
{
    public interface ICustomerBusinessRules
    {
        IEnumerable<int> ValidateUniqueIds(IEnumerable<CustomerDto> newDtos, IEnumerable<Customer> existingCustomers);
    }
}
