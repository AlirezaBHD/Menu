using Application.Dto.MenuItem;
using Application.Extensions;
using Application.Validations.ActivityPeriod;
using FluentValidation;

namespace Application.Validations.MenuItem;

public class UpdateMenuItemRequestValidator : AbstractValidator<UpdateMenuItemRequest>
{
    public UpdateMenuItemRequestValidator()
    {
        // RuleFor(c => c.ImageFile)!.ImageFileRule(blank: true);
        
        RuleFor(x => x.ActivityPeriod)
            .NotNull().WithMessage("دوره دسترسی الزامی است")
            .SetValidator(new ActivityPeriodDtoValidator());
        
        RuleForEach(x => x.Translations)
            .SetValidator(new MenuItemTranslationValidator());
    }
}