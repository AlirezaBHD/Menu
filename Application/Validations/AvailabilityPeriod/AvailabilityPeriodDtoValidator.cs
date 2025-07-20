using Application.Dto.AvailabilityPeriod;
using Domain.Entities;
using FluentValidation;

namespace Application.Validations.AvailabilityPeriod;


public class AvailabilityPeriodDtoValidator : AbstractValidator<AvailabilityPeriodRequest>
{
    public AvailabilityPeriodDtoValidator()
    {
        var entityType = typeof(Domain.Entities.AvailabilityPeriod);
        RuleFor(ap => ap.IsAvailable)
            .NotNull().WithMessage("وضعیت فعال بودن الزامی است");
        
        RuleFor(ap => ap.ActivatyEnum)!.IsInEnum().WithMessage("مقدار وارد شده برای (وضعیت فعالیت) اشتباه است");
        
        When(ap => ap.ActivatyEnum != ActivityEnum.Unlimited, () =>
        {
            RuleFor(ap => ap.FromTime)
                .NotNull().WithMessage("ساعت شروع بازه الزامی است")
                .NotEmpty().WithMessage("ساعت شروع بازه نمیتواند خالی باشد")
                .Must(time => TimeSpan.TryParse(time.ToString(), out _))
                .WithMessage("فرمت زمان نادرست است (مثال: 08:30 یا 15:45:00)")
                .LessThan(ap => ap.ToTime)
                .WithMessage("ساعت شروع باید کوچکتر از ساعت پایان باشد.");

            RuleFor(ap => ap.FromTime)
                .NotNull().WithMessage("ساعت پایان بازه الزامی است")
                .NotEmpty().WithMessage("ساعت پایان بازه نمیتواند خالی باشد")
                .Must(time => TimeSpan.TryParse(time.ToString(), out _))
                .WithMessage("فرمت زمان نادرست است (مثال: 08:30 یا 15:45:00)");

            RuleFor(ap => ap)
                .Must(ap => ap.FromTime != ap.ToTime)
                .WithMessage("ساعت شروع و پایان نباید یکسان باشند.");
        });
    }
}