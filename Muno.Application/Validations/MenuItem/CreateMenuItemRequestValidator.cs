using FluentValidation;
using Muno.Application.Dto.MenuItem;
using Muno.Application.Localization;
using Muno.Application.Validations.ActivityPeriod;
using Muno.Application.Validations.MenuItemVariant;

namespace Muno.Application.Validations.MenuItem;

public class CreateMenuItemRequestValidator : AbstractValidator<CreateMenuItemRequest>
{
    public CreateMenuItemRequestValidator()
    {
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