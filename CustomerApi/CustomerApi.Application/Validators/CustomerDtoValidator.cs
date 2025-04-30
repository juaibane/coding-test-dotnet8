using CustomerApi.Application.Dtos;
using FluentValidation;

namespace CustomerApi.Application.Validators
{
    public class CustomerDtoValidator : AbstractValidator<CustomerDto>
    {
        public CustomerDtoValidator()
        {
            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage("First name is required.");

            RuleFor(c => c.LastName)
                .NotEmpty().WithMessage("Last name is required.");

            RuleFor(c => c.Age)
                .GreaterThan(18).WithMessage("Customer must be over 18 years old.");

            RuleFor(c => c.Id)
                .GreaterThan(0).WithMessage("Id must be a positive integer.");
        }
    }
}
