using Application.Dto.Category;
using Application.Extensions;
using FluentValidation;

namespace Application.Validations.Category;

public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryRequestValidator()
    {
        var entityType = typeof(Domain.Entities.Category);
        
        RuleFor(x => x.Title)!
            .ApplyLengthValidation(x => x.Title!, entityType);
    }
}