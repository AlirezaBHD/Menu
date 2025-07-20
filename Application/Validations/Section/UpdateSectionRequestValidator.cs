using Application.Dto.Section;
using Application.Extensions;
using Application.Validations.ActivityPeriod;
using FluentValidation;

namespace Application.Validations.Section;

public class UpdateSectionRequestValidator : AbstractValidator<UpdateSectionRequest>
{
    public UpdateSectionRequestValidator()
    {
        var entityType = typeof(Domain.Entities.Section);

        RuleFor(c => c.Title)!
            .LengthValidationRule(dto => dto.Title!, entityType);
        
        RuleFor(x => x.ActivityPeriod)
            .NotNull().WithMessage("دوره دسترسی الزامی است")
            .SetValidator(new ActivityPeriodDtoValidator());
    }
}