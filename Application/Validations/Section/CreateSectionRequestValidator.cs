using Application.Dto.Section;
using Application.Extensions;
using FluentValidation;

namespace Application.Validations.Section;

public class CreateSectionRequestValidator : AbstractValidator<CreateSectionRequest>
{
    public CreateSectionRequestValidator()
    {
        var entityType = typeof(Domain.Entities.Section);
        
        RuleFor(c => c.Title)!
            .ApplyLengthValidation(dto => dto.Title!, entityType);
    }
}