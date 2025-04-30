using CustomerApi.Application.Dtos;
using CustomerApi.Application.Validators.Business;
using CustomerApi.Application.Validators.Business.Interface;
using CustomerApi.Domain.Entities;
using Xunit;

namespace CustomerApi.Tests.Validations
{
    public class CustomerBusinessRulesTests
    {
        private readonly ICustomerBusinessRules _rules = new CustomerBusinessRules();

        [Fact]
        public void ValidateUniqueIds_NoErrors_WhenIdsAreUnique()
        {
            // Arrange
            var existing = new List<Customer> { new Customer { Id = 1 }, new Customer { Id = 2 } };
            var dtos = new List<CustomerDto> { new CustomerDto { Id = 3 } };

            // Act
            var errors = _rules.ValidateUniqueIds(dtos, existing);

            // Assert
            Assert.Empty(errors);
        }

        [Fact]
        public void ValidateUniqueIds_ReturnsErrors_WhenIdsDuplicated()
        {
            // Arrange
            var existing = new List<Customer> { new Customer { Id = 1 }, new Customer { Id = 2 } };
            var dtos = new List<CustomerDto>
            {
                new CustomerDto { Id = 2 },
                new CustomerDto { Id = 1 },
                new CustomerDto { Id = 2 }
            };

            // Act
            var errors = _rules.ValidateUniqueIds(dtos, existing);

            // Assert
            Assert.Equal(3, errors.Count());
            Assert.Equal(2, errors.ElementAt(0));
            Assert.Equal(1, errors.ElementAt(1));
            Assert.Equal(2, errors.ElementAt(2));
        }
    }
}
