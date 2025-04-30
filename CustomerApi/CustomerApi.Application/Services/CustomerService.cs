using CustomerApi.Application.Common.Core;
using CustomerApi.Application.Common.Utils;
using CustomerApi.Application.Dtos;
using CustomerApi.Application.Mappers;
using CustomerApi.Application.Services.Interfaces;
using CustomerApi.Application.Validators.Business.Interface;
using CustomerApi.Infrastructure.Persistence.Interfaces;
using FluentValidation;

namespace CustomerApi.Application.Services
{
    public class CustomerService(ICustomerRepository repository, IValidator<List<CustomerDto>> validator,
      ICustomerBusinessRules businessRules) : ICustomerService
    {
        private readonly ICustomerBusinessRules _businessRules = businessRules;
        private readonly ICustomerRepository _repository = repository;
        private readonly IValidator<List<CustomerDto>> _validator = validator;

        public async Task<OperationResult<List<CustomerDto>>> GetAllAsync()
        {
            var customers = await _repository.GetAllAsync();
            var dtos = CustomerToDtoMapper.Map(customers);
            return OperationResult<List<CustomerDto>>.Ok(dtos);
        }

        public async Task<OperationResult> AddCustomersAsync(List<CustomerDto> customerDtos)
        {
            var dataValidations = await _validator.ValidateAsync(customerDtos);
            if (!dataValidations.IsValid)
                return OperationResult.BadRequest(dataValidations.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}").ToList());

            var existingCustomers = await _repository.GetAllAsync();
            var duplicateIds =_businessRules.ValidateUniqueIds(customerDtos, existingCustomers);
            var duplicateIdErrors = duplicateIds.Select(id => $"ID {id} already exists").ToList();
            if (duplicateIdErrors.Any())
                return OperationResult.BadRequest(duplicateIdErrors);

            var customers = CustomerFromDtoMapper.Map(customerDtos);
            CustomerUtils.ResetOrder(existingCustomers);
            CustomerUtils.InsertSorted(customers, existingCustomers);
            await _repository.ReplaceData(customers);

            return OperationResult.Created();
        }
    }
}