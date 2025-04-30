using CustomerApi.Application.Common.Core;
using CustomerApi.Application.Dtos;
using CustomerApi.Domain.Entities;

namespace CustomerApi.Application.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<OperationResult> AddCustomersAsync(List<CustomerDto> customerDtos);
        Task<OperationResult<List<CustomerDto>>> GetAllAsync();
    }
}