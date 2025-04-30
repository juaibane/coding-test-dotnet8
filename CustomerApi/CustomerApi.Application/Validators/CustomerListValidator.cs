using CustomerApi.Application.Dtos;
using FluentValidation;

namespace CustomerApi.Application.Validators
{
    public class CustomerListValidator : AbstractValidator<List<CustomerDto>>
    {
        public CustomerListValidator(IValidator<CustomerDto> customerValidator)
        {
            RuleForEach(x => x)
                .SetValidator(customerValidator);
        }
    }
}
