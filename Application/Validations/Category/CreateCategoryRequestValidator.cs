using Application.Dto.Category;
using Application.Extensions;
using Application.Localization;
using Application.Validations.ActivityPeriod;
using FluentValidation;

namespace Application.Validations.Category;

public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryRequestValidator()
    {
        RuleFor(x => x.ActivityPeriod)
            .NotNull().WithMessage(Resources.RequiredActivityPeriod)
            .SetValidator(new ActivityPeriodDtoValidator());
        
        RuleForEach(x => x.Translations)
            .SetValidator(new CategoryTranslationValidator());
    }
}