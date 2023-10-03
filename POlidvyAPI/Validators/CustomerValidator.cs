using FluentValidation;
using POlidvyAPI.ViewModels;

namespace POlidvyAPI.Validators
{
     public class CustomerValidator : AbstractValidator<CustomerViewModel>
    {
        public CustomerValidator()
        {
            RuleFor(customer => customer.CustomerFirstName).NotNull().NotEmpty();
            RuleFor(customer => customer.CustomerLastName).NotNull().NotEmpty();
            RuleFor(customer => customer.CustomerAddress).NotNull().NotEmpty();
            RuleFor(customer => customer.CustomerBirthDate).NotNull().NotEmpty()
                .LessThan(customer => DateTime.UtcNow);
            RuleFor(customer => customer.CustomerContactNo).NotNull().NotEmpty().MinimumLength(10)
                .MaximumLength(10).WithMessage("{PropertyName} must have 10 digits.");
            RuleFor(customer => customer.CustomerEmail).NotNull().NotEmpty()
                .EmailAddress();
            RuleFor(customer => customer.CustomerSalary).NotNull().NotEmpty()
                .GreaterThanOrEqualTo(400000)
                .WithMessage("{PropertyName} must be greater than 400000.");
            RuleFor(customer => customer.EmployerTypeId).NotNull().NotEmpty()
                .InclusiveBetween(1, 2).WithMessage("{PropertyName} must be 1 or 2.");
            RuleFor(customer => customer.EmployerName).NotEmpty().Unless(c => c.EmployerTypeId == 2)
                .WithMessage("{PropertyName} is required when employer id is 1.");
        }
    }
}
