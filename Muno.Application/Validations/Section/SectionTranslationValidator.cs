using Muno.Application.Extensions;
using Domain.Entities.Sections;
using FluentValidation;
using Muno.Application.Dto.Section;

namespace Muno.Application.Validations.Section;


public class SectionTranslationValidator : AbstractValidator<SectionTranslationDto>
{
    public SectionTranslationValidator()
    {
        var entityType = typeof(SectionTranslation);
        
        RuleFor(c => c.Title)!
            .LengthValidationRule(dto => dto.Title, entityType);
    }
}