using CustomerApi.Application.Dtos;
using CustomerApi.Domain.Entities;

namespace CustomerApi.Application.Mappers
{
    public static class CustomerToDtoMapper
    {
        public static List<CustomerDto> Map(List<Customer> customers)
        {
            return customers.Select(x => Map(x)).ToList();
        }

        public static CustomerDto Map(Customer customer)
        {
            return new CustomerDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Age = customer.Age
            };
        }
    }
}
