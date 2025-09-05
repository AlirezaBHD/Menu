using Application.Dto.MenuItem;
using Application.Extensions;
using Application.Localization;
using Application.Validations.ActivityPeriod;
using FluentValidation;

namespace Application.Validations.MenuItem;

public class UpdateMenuItemRequestValidator : AbstractValidator<UpdateMenuItemRequest>
{
    public UpdateMenuItemRequestValidator()
    {
        // RuleFor(c => c.ImageFile)!.ImageFileRule(blank: true);
        
        RuleFor(x => x.ActivityPeriod)
            .NotNull().WithMessage(Resources.RequiredActivityPeriod)
            .SetValidator(new ActivityPeriodDtoValidator());
        
        RuleForEach(x => x.Translations)
            .SetValidator(new MenuItemTranslationValidator());
    }
}