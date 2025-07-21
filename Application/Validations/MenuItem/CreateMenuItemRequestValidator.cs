using Application.Dto.MenuItem;
using Application.Extensions;
using Application.Validations.ActivityPeriod;
using Application.Validations.MenuItemVariant;
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

        RuleFor(c => c.ImageFile).ImageFileRule();
        
        RuleFor(x => x.ActivityPeriod)
            .NotNull().WithMessage("دوره دسترسی الزامی است")
            .SetValidator(new ActivityPeriodDtoValidator());
        
        RuleForEach(x => x.Variants)
            .SetValidator(new MenuItemVariantDtoValidator());
        
        RuleFor(x => x)
            .Custom((model, context) =>
            {
                if (!model.IsAvailable)
                {
                    if (model.Variants.Any(v => v.IsAvailable))
                    {
                        context.AddFailure(
                            "آیتمی که غیرقابل ارائه است، نمیتواند دارای نوعی باشد که در دسترس است");
                    }
                }
                else
                {
                    if (model.Variants.All(v => !v.IsAvailable))
                    {
                        context.AddFailure("آیتم قابل ارائه نباید شامل نوع هایی باشد که هیچ کدام در دسترس نیستند");
                    }
                }
            });
    }
}