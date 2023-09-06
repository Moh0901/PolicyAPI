using FluentValidation;
using Microsoft.EntityFrameworkCore.Metadata;
using POlidvyAPI.Model.ViewModel;

namespace POlidvyAPI.Validators
{
    public class PolicyValidator : AbstractValidator<PolicyViewModel>
    {
        public PolicyValidator()
        {
            RuleFor(policy => policy.PolicyName).NotNull().NotEmpty()
                .WithMessage("Policy Name cannot be empty.");
            RuleFor(policy => policy.PolicyStartDate).NotNull().NotEmpty();
            RuleFor(policy => policy.PolicyDuration).NotNull().NotEmpty().GreaterThanOrEqualTo(1)
                .WithMessage("Policy duration cannot be zero.");
            RuleFor(policy => policy.PolicyInitialDeposit).NotNull().NotEmpty();
            RuleFor(policy => policy.PolicyTypeId).NotNull().NotEmpty()
               .InclusiveBetween(1, 6).WithMessage("Policy Type Id must in between 1 and 6.");
            RuleFor(policy => policy.UserTypeId).NotNull().NotEmpty()
               .InclusiveBetween(1, 5).WithMessage("User Type Id must in between 1 and 5.");
            RuleFor(policy => policy.PolicyInterest).NotNull().NotEmpty().NotEqual("0");
            RuleFor(policy => policy.PolicyAmount).NotNull().NotEmpty()
                .WithMessage("Policy Amount cannot be empty.");
            RuleFor(policy => policy.PolicyTermsPerYear).NotNull().NotEmpty().NotEqual("0")
                .WithMessage("Policy Terms cannnot be empty and zero.");
            RuleFor(policy => policy.PolicyInterest).NotNull().NotEmpty().NotEqual("0")
                .WithMessage("Policy Interest cannnot be empty and zero.");

        }
    }
}
