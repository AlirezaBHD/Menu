using Application.Dto.MenuItemVariant;
using Application.Extensions;
using Application.Localization;
using Domain.Entities.MenuItemVariants;
using FluentValidation;

namespace Application.Validations.MenuItemVariant;

public class MenuItemVariantTranslationValidator : AbstractValidator<MenuItemVariantTranslationDto>
{
    public MenuItemVariantTranslationValidator()
    {
        var entityType = typeof(MenuItemVariantTranslation);

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage(Resources.PriceGreaterThanZero)
            .LessThan(1_000_000_000).WithMessage(Resources.PriceLessThanOneBillion)
            .PrecisionScale(18, 0, true).WithMessage(Resources.PriceMustBeInteger);

        
        RuleFor(c => c.Detail)!
            .LengthValidationRule(dto => dto.Detail!, entityType, blank: true);
    }
}