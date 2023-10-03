using FluentValidation;
using POlidvyAPI.ViewModels;

namespace POlidvyAPI.Validators
{
    public class PolicyValidator : AbstractValidator<PolicyViewModel>
    {
        public PolicyValidator()
        {
            RuleFor(policy => policy.PolicyName).NotNull().NotEmpty()
                .WithMessage("{PropertyName} cannot be empty.");
            RuleFor(policy => policy.PolicyStartDate).NotNull().NotEmpty()
                .Equals(DateTime.Now);
            RuleFor(policy => policy.PolicyDuration).NotNull().NotEmpty()
                .GreaterThanOrEqualTo(1)
                .WithMessage("{PropertyName} cannot be zero.");
            RuleFor(policy => policy.PolicyInitialDeposit).NotNull().NotEmpty()
                .GreaterThanOrEqualTo(3000)
                .WithMessage("{PropertyName} cznnot be less than 3000.");
            RuleFor(policy => policy.PolicyTypeId).NotNull().NotEmpty()
               .InclusiveBetween(1, 6)
               .WithMessage("{PropertyName} must in between 1 and 6.");
            RuleFor(policy => policy.UserTypeId).NotNull().NotEmpty()
               .InclusiveBetween(1, 5)
               .WithMessage("{PropertyName} must in between 1 and 5.");
            RuleFor(policy => policy.PolicyInterest).NotNull().NotEmpty().GreaterThan(0)
                .WithMessage("{PropertyName} cannnot be empty and zero.");
            RuleFor(policy => policy.PolicyAmount).NotNull().NotEmpty()
                .WithMessage("{PropertyName} cannot be empty.");
            RuleFor(policy => policy.PolicyTermsPerYear).NotNull().NotEmpty().GreaterThan(0)
                .WithMessage("{PropertyName} cannnot be empty and zero.");

        }
    }
}
