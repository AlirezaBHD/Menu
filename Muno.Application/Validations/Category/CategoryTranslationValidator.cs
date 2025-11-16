using Muno.Application.Extensions;
using Muno.Application.Validations.ActivityPeriod;
using Muno.Domain.Entities.Categories;
using FluentValidation;
using Muno.Application.Dto.Category;

namespace Muno.Application.Validations.Category;


public class CategoryTranslationValidator : AbstractValidator<CategoryTranslationDto>
{
    public CategoryTranslationValidator()
    {
        var entityType = typeof(CategoryTranslation);
        
        RuleFor(c => c.Title)!
            .LengthValidationRule(dto => dto.Title, entityType);
    }
}