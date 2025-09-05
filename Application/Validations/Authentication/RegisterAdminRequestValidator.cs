using Application.Dto.Authentication;
using Application.Localization;
using FluentValidation;

namespace Application.Validations.Authentication;

public class RegisterAdminRequestValidator : AbstractValidator<RegisterAdminRequest>
{
    public RegisterAdminRequestValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage(Resources.RequiredUsername)
            .MinimumLength(3).WithMessage(Resources.MinLengthUsername)
            .MaximumLength(20).WithMessage(Resources.MaxLengthUsername)
            .Matches("^[a-zA-Z0-9_]+$").WithMessage(Resources.UsernamePattern);

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(Resources.RequiredPassword)
            .MinimumLength(6).WithMessage(Resources.MinLengthPassword)
            .Matches("[A-Z]").WithMessage(Resources.PasswordUppercase)
            .Matches("[a-z]").WithMessage(Resources.PasswordLowercase)
            .Matches("[0-9]").WithMessage(Resources.PasswordNumber)
            .Matches("[^a-zA-Z0-9]").WithMessage(Resources.PasswordSpecial);
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(Resources.RequiredEmail)
            .EmailAddress().WithMessage(Resources.InvalidEmail);
        
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage(Resources.RequiredPhoneNumber)
            .Matches(@"^09\d{9}$").WithMessage(Resources.PhoneNumberPattern);
    }
}