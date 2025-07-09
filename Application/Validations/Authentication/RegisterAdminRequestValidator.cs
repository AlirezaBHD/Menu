using Application.Dto.Authentication;
using FluentValidation;

namespace Application.Validations.Authentication;

public class RegisterAdminRequestValidator : AbstractValidator<RegisterAdminRequest>
{
    public RegisterAdminRequestValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("نام کاربری الزامی است.")
            .MinimumLength(3).WithMessage("نام کاربری باید حداقل ۳ کاراکتر باشد.")
            .MaximumLength(20).WithMessage("نام کاربری نباید بیش از ۲۰ کاراکتر باشد.")
            .Matches("^[a-zA-Z0-9_]+$").WithMessage("نام کاربری فقط می‌تواند شامل حروف، عدد و _ باشد.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("رمز عبور الزامی است.")
            .MinimumLength(6).WithMessage("رمز عبور باید حداقل ۶ کاراکتر باشد.")
            .Matches("[A-Z]").WithMessage("رمز عبور باید حداقل یک حرف بزرگ داشته باشد.")
            .Matches("[a-z]").WithMessage("رمز عبور باید حداقل یک حرف کوچک داشته باشد.")
            .Matches("[0-9]").WithMessage("رمز عبور باید حداقل یک رقم داشته باشد.")
            .Matches("[^a-zA-Z0-9]").WithMessage("رمز عبور باید حداقل یک کاراکتر خاص داشته باشد.");
    }
}
