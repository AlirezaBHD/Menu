using FluentValidation;
using Muno.Application.Dto.Category;
using Muno.Application.Localization;
using Muno.Application.Validations.ActivityPeriod;

namespace Muno.Application.Validations.Category;

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