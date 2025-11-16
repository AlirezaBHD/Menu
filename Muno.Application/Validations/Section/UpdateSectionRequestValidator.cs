using FluentValidation;
using Muno.Application.Dto.Section;
using Muno.Application.Localization;
using Muno.Application.Validations.ActivityPeriod;

namespace Muno.Application.Validations.Section;

public class UpdateSectionRequestValidator : AbstractValidator<UpdateSectionRequest>
{
    public UpdateSectionRequestValidator()
    {
        RuleFor(x => x.ActivityPeriod)
            .NotNull().WithMessage(Resources.RequiredActivityPeriod)
            .SetValidator(new ActivityPeriodDtoValidator());
        
        RuleForEach(x => x.Translations)
            .SetValidator(new SectionTranslationValidator());
    }
}