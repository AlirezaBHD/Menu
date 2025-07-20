using Application.Dto.MenuItem;
using Application.Extensions;
using Application.Validations.ActivityPeriod;
using FluentValidation;

namespace Application.Validations.MenuItem;

public class CreateMenuItemRequestValidator : AbstractValidator<CreateMenuItemRequest>
{
    public CreateMenuItemRequestValidator()
    {
        var entityType = typeof(Domain.Entities.MenuItem);

        RuleFor(c => c.Title)!
            .LengthValidationRule(dto => dto.Title!, entityType);

        RuleFor(c => c.Description)!
            .LengthValidationRule(dto => dto.Description!, entityType, blank: true);

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("قیمت باید بیشتر از صفر باشد.")
            .LessThan(1_000_000_000).WithMessage("قیمت نمی‌تواند بیش از یک میلیارد ریال باشد.")
            .PrecisionScale(18, 0, true).WithMessage("قیمت باید عددی صحیح باشد.");

        RuleFor(c => c.ImageFile).ImageFileRule();
        
        RuleFor(x => x.ActivityPeriod)
            .NotNull().WithMessage("دوره دسترسی الزامی است")
            .SetValidator(new ActivityPeriodDtoValidator());
    }
}