using Application.Dto.Section;
using Application.Extensions;
using Application.Validations.AvailabilityPeriod;
using FluentValidation;

namespace Application.Validations.Section;

public class CreateSectionRequestValidator : AbstractValidator<CreateSectionRequest>
{
    public CreateSectionRequestValidator()
    {
        var entityType = typeof(Domain.Entities.Section);
        
        RuleFor(c => c.Title)!
            .LengthValidationRule(dto => dto.Title!, entityType);
        
        RuleFor(x => x.AvailabilityPeriod)
            .NotNull().WithMessage("دوره دسترسی الزامی است")
            .SetValidator(new AvailabilityPeriodDtoValidator());
    }
}