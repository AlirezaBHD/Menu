using Muno.Application.Extensions;
using FluentValidation;
using Muno.Application.Dto.Category;
using Muno.Application.Localization;
using Muno.Application.Validations.ActivityPeriod;

namespace Muno.Application.Validations.Category;

public class UpdateCategoryRequestValidator : AbstractValidator<UpdateCategoryRequest>
{
    public UpdateCategoryRequestValidator()
    {
        RuleFor(x => x.ActivityPeriod)
            .NotNull().WithMessage(Resources.RequiredActivityPeriod)
            .SetValidator(new ActivityPeriodDtoValidator());
        
        RuleForEach(x => x.Translations)
            .SetValidator(new CategoryTranslationValidator());
    }
}