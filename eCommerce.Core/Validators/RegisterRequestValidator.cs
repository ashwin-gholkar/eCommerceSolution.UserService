using eCommerce.Core.DTO;
using FluentValidation;

namespace eCommerce.Core.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(temp => temp.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");
            RuleFor(temp => temp.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long");
            RuleFor(temp => temp.PersonName)
                .NotEmpty().WithMessage("Person name is required")
                .MaximumLength(50).WithMessage("Person name must not exceed 50 characters")
                .MinimumLength(1).WithMessage("Person name must not be less than 1 character");
            RuleFor(temp=> temp.Gender)
                .NotEmpty().WithMessage("Gender is required")
                .IsInEnum().WithMessage("Invalid gender selected")
                .Must(i=> Enum.IsDefined(typeof(GenderOptions),i))
                    .WithMessage("Gender selected is not in the options.");

        }
    }
}
