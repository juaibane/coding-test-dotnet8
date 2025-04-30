using CustomerApi.Application.Dtos;
using CustomerApi.Application.Validators.Business.Interface;
using CustomerApi.Domain.Entities;

namespace CustomerApi.Application.Validators.Business
{
    public class CustomerBusinessRules : ICustomerBusinessRules
    {
        public IEnumerable<int> ValidateUniqueIds(IEnumerable<CustomerDto> newDtos, IEnumerable<Customer> existingCustomers)
        {
            var existingCustomersId = existingCustomers.Select(c => c.Id).ToHashSet();
            var errors = newDtos.Where(d => existingCustomersId.Contains(d.Id)).Select(d =>d.Id);
            return errors;
        }
    }
}
