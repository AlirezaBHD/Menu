using Application.Dto.Category;
using Application.Extensions;
using FluentValidation;

namespace Application.Validations.Category;

public class UpdateCategoryRequestValidator : AbstractValidator<UpdateCategoryRequest>
{
    public UpdateCategoryRequestValidator()
    {
        var entityType = typeof(Domain.Entities.Category);
        
        RuleFor(c => c.Title)!
            .LengthValidationRule(dto => dto.Title!, entityType);
    }
}