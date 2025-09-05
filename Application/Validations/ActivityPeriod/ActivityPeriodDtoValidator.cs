using Application.Dto.ActivityPeriod;
using Application.Localization;
using Domain.Entities;
using FluentValidation;

namespace Application.Validations.ActivityPeriod;

public class ActivityPeriodDtoValidator : AbstractValidator<ActivityPeriodRequest>
{
    public ActivityPeriodDtoValidator()
    {
        RuleFor(ap => ap.IsActive)
            .NotNull()
            .WithMessage(Resources.RequiredIsActive);

        RuleFor(ap => ap.ActivityEnum)
            .IsInEnum()
            .WithMessage(Resources.InvalidActivityEnum);

        When(ap => ap.ActivityEnum != ActivityEnum.Unlimited, () =>
        {
            RuleFor(ap => ap.FromTime)
                .NotNull().WithMessage(Resources.RequiredFromTime)
                .NotEmpty().WithMessage(Resources.EmptyFromTime)
                .Must(time => TimeSpan.TryParse(time.ToString(), out _))
                .WithMessage(Resources.InvalidTimeFormat)
                .LessThan(ap => ap.ToTime)
                .WithMessage(Resources.FromTimeLessThanToTime);

            RuleFor(ap => ap.ToTime)
                .NotNull().WithMessage(Resources.RequiredToTime)
                .NotEmpty().WithMessage(Resources.EmptyToTime)
                .Must(time => TimeSpan.TryParse(time.ToString(), out _))
                .WithMessage(Resources.InvalidTimeFormat);

            RuleFor(ap => ap)
                .Must(ap => ap.FromTime != ap.ToTime)
                .WithMessage(Resources.FromTimeNotEqualToTime);
        });
    }
}
