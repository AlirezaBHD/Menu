using Application.Dto.MenuItem;
using Application.Validations.ActivityPeriod;
using Application.Validations.MenuItemVariant;
using FluentValidation;

namespace Application.Validations.MenuItem;

public class CreateMenuItemRequestValidator : AbstractValidator<CreateMenuItemRequest>
{
    public CreateMenuItemRequestValidator()
    {
        // RuleFor(c => c.ImageFile)!.ImageFileRule(blank: true);
        
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
        
        RuleForEach(x => x.Translations)
            .SetValidator(new MenuItemTranslationValidator());
    }
}