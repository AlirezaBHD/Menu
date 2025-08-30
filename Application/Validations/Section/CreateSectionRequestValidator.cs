using Application.Dto.Section;
using Application.Extensions;
using Application.Validations.ActivityPeriod;
using FluentValidation;

namespace Application.Validations.Section;

public class CreateSectionRequestValidator : AbstractValidator<CreateSectionRequest>
{
    public CreateSectionRequestValidator()
    {
        var entityType = typeof(Domain.Entities.Sections.Section);
        
        RuleFor(c => c.Title)!
            .LengthValidationRule(dto => dto.Title!, entityType);
        
        RuleFor(x => x.ActivityPeriod)
            .NotNull().WithMessage("دوره دسترسی الزامی است")
            .SetValidator(new ActivityPeriodDtoValidator());
    }
}