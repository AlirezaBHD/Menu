using Application.Dto.Section;
using Application.Extensions;
using Domain.Entities.Sections;
using FluentValidation;

namespace Application.Validations.Section;


public class SectionTranslationValidator : AbstractValidator<SectionTranslationDto>
{
    public SectionTranslationValidator()
    {
        var entityType = typeof(SectionTranslation);
        
        RuleFor(c => c.Title)!
            .LengthValidationRule(dto => dto.Title, entityType);
    }
}