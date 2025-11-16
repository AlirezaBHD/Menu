using FluentValidation;
using Muno.Application.Dto.MenuItem;
using Muno.Application.Localization;
using Muno.Application.Validations.ActivityPeriod;

namespace Muno.Application.Validations.MenuItem;

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