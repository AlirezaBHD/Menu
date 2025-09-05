using Application.Dto.MenuItem;
using Application.Localization;
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
            .NotNull().WithMessage(Resources.RequiredActivityPeriod)
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
                        context.AddFailure(Resources.UnavailableItemType);

                    }
                }
                else
                {
                    if (model.Variants.All(v => !v.IsAvailable))
                    {
                        context.AddFailure(Resources.InvalidItemTypesCombination);
                    }
                }
            });
        
        RuleForEach(x => x.Translations)
            .SetValidator(new MenuItemTranslationValidator());
    }
}