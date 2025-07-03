using Application.Dto.Section;
using Application.Extensions;
using FluentValidation;

namespace Application.Validations.Section;

public class UpdateSectionRequestValidator : AbstractValidator<UpdateSectionRequest>
{
    public UpdateSectionRequestValidator()
    {
        var entityType = typeof(Domain.Entities.Section);

        RuleFor(c => c.Title)!
            .LengthValidationRule(dto => dto.Title!, entityType);
    }
}