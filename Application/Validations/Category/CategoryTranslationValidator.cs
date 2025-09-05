using Application.Dto.Category;
using Application.Extensions;
using Application.Validations.ActivityPeriod;
using Domain.Entities.Categories;
using FluentValidation;

namespace Application.Validations.Category;


public class CategoryTranslationValidator : AbstractValidator<CategoryTranslationDto>
{
    public CategoryTranslationValidator()
    {
        var entityType = typeof(CategoryTranslation);
        
        RuleFor(c => c.Title)!
            .LengthValidationRule(dto => dto.Title, entityType);
    }
}