using Application.Dto.MenuItemVariant;
using Application.Extensions;
using Domain.Entities.MenuItemVariants;
using FluentValidation;

namespace Application.Validations.MenuItemVariant;

public class MenuItemVariantTranslationValidator : AbstractValidator<MenuItemVariantTranslationDto>
{
    public MenuItemVariantTranslationValidator()
    {
        var entityType = typeof(MenuItemVariantTranslation);

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("قیمت باید بیشتر از صفر باشد.")
            .LessThan(1_000_000_000).WithMessage("قیمت نمی‌تواند بیش از یک میلیارد ریال باشد.")
            .PrecisionScale(18, 0, true).WithMessage("قیمت باید عددی صحیح باشد.");
        
        RuleFor(c => c.Detail)!
            .LengthValidationRule(dto => dto.Detail!, entityType, blank: true);
    }
}