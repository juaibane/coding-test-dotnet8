using CustomerApi.Application.Dtos;
using CustomerApi.Application.Services;
using CustomerApi.Application.Validators;
using CustomerApi.Application.Validators.Business.Interface;
using CustomerApi.Application.Validators.Business;
using CustomerApi.Domain.Entities;
using CustomerApi.Infrastructure.Persistence.Interfaces;
using Moq;
using Xunit;

namespace CustomerApi.Tests.Services
{
    public class CustomerServiceTests
    {
        private readonly Mock<ICustomerRepository> _customerRepoMock;
        private readonly CustomerListValidator _listValidator;
        private readonly ICustomerBusinessRules _businessRules;

        public CustomerServiceTests()
        {
            _customerRepoMock = new Mock<ICustomerRepository>();
            _listValidator = new CustomerListValidator(new CustomerDtoValidator());
            _businessRules = new CustomerBusinessRules();
        }

        [Fact]
        public async Task CreateCustomersAsync_Fails_WhenDtoValidationFails()
        {
            var dtos = new List<CustomerDto>
            {
                new CustomerDto { Id = 0, FirstName = "", LastName = "", Age = 17 }
            };
            _customerRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Customer>());

            var svc = new CustomerService(_customerRepoMock.Object, _listValidator, _businessRules);
            var result = await svc.AddCustomersAsync(dtos);

            Assert.False(result.Success);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public async Task CreateCustomersAsync_Fails_WhenIdAlreadyExists()
        {
            var existing = new List<Customer> { new Customer { Id = 5, FirstName = "A", LastName = "B", Age = 30 } };
            _customerRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(existing);

            var dtos = new List<CustomerDto>
            {
                new CustomerDto { Id = 5, FirstName = "X", LastName = "Y", Age = 25 }
            };

            var svc = new CustomerService(_customerRepoMock.Object, _listValidator, _businessRules);
            var result = await svc.AddCustomersAsync(dtos);

            Assert.False(result.Success);
            Assert.Contains("ID 5 already exists", result.Errors);
        }

        [Fact]
        public async Task CreateCustomersAsync_Succeeds_WhenAllValid()
        {
            _customerRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Customer>());
            _customerRepoMock.Setup(r => r.ReplaceData(It.IsAny<List<Customer>>()))
                     .Returns(Task.CompletedTask);

            var dtos = new List<CustomerDto>
            {
                new CustomerDto { Id = 1, FirstName = "A", LastName = "B", Age = 30 },
                new CustomerDto { Id = 2, FirstName = "B", LastName = "B", Age = 25 }
            };

            var svc = new CustomerService(_customerRepoMock.Object, _listValidator, _businessRules);
            var result = await svc.AddCustomersAsync(dtos);

            Assert.True(result.Success);
            _customerRepoMock.Verify(r => r.ReplaceData(It.IsAny<List<Customer>>()), Times.Once);
        }

        [Fact]
        public async Task GetAllCustomersAsync_ReturnsMappedDtos()
        {
            var existing = new List<Customer>
            {
                new Customer { Id = 1, FirstName = "Foo", LastName = "Bar", Age = 40 }
            };
            _customerRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(existing);

            var svc = new CustomerService(_customerRepoMock.Object, _listValidator, _businessRules);
            var result = await svc.GetAllAsync();
            var data = result.Data;

            Assert.Single(data);
            Assert.Equal(1, data[0].Id);
            Assert.Equal("Foo", data[0].FirstName);
            Assert.Equal("Bar", data[0].LastName);
            Assert.Equal(40, data[0].Age);
        }
    }
}