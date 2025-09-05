using Application.Dto.Section;
using Application.Extensions;
using Application.Localization;
using Application.Validations.ActivityPeriod;
using FluentValidation;

namespace Application.Validations.Section;

public class CreateSectionRequestValidator : AbstractValidator<CreateSectionRequest>
{
    public CreateSectionRequestValidator()
    {
        RuleFor(x => x.ActivityPeriod)
            .NotNull().WithMessage(Resources.RequiredActivityPeriod)
            .SetValidator(new ActivityPeriodDtoValidator());
        
        RuleForEach(x => x.Translations)
            .SetValidator(new SectionTranslationValidator());
    }
}