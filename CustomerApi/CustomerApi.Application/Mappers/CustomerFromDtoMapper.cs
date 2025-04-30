using CustomerApi.Application.Dtos;
using CustomerApi.Domain.Entities;

namespace CustomerApi.Application.Mappers
{
    public static class CustomerFromDtoMapper
    {
        public static List<Customer> Map(List<CustomerDto> customerDtos)
        {
            return customerDtos.Select(x=> Map(x)).ToList();
        }

        public static Customer Map(CustomerDto customerDto)
        {
            return new Customer
            {
                Id = customerDto.Id,
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName,
                Age = customerDto.Age
            };
        }
    }

}
