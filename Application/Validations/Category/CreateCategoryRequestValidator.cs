using Application.Dto.Category;
using Application.Extensions;
using Application.Validations.ActivityPeriod;
using FluentValidation;

namespace Application.Validations.Category;

public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryRequestValidator()
    {
        var entityType = typeof(Domain.Entities.Category);
        
        RuleFor(c => c.Title)!
            .LengthValidationRule(dto => dto.Title!, entityType);
        
        RuleFor(x => x.ActivityPeriod)
            .NotNull().WithMessage("دوره دسترسی الزامی است")
            .SetValidator(new ActivityPeriodDtoValidator());
    }
}