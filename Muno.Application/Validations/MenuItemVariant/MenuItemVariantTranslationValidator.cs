using Muno.Application.Extensions;
using Muno.Domain.Entities.MenuItemVariants;
using FluentValidation;
using Muno.Application.Dto.MenuItemVariant;
using Muno.Application.Localization;

namespace Muno.Application.Validations.MenuItemVariant;

public class MenuItemVariantTranslationValidator : AbstractValidator<MenuItemVariantTranslationDto>
{
    public MenuItemVariantTranslationValidator()
    {
        var entityType = typeof(MenuItemVariantTranslation);

        RuleFor(x => x.Price)
            .NotEmpty().WithMessage(Resources.PriceMustBeInteger)
            .Must(BeValidIntegerPrice).WithMessage(Resources.PriceMustBeInteger);

        
        
        RuleFor(c => c.Detail)!
            .LengthValidationRule(dto => dto.Detail!, entityType, blank: true);
    }
    private bool BeValidIntegerPrice(string price)
    {
        if (string.IsNullOrWhiteSpace(price) || !decimal.TryParse(price, out decimal value))
        {
            return false;
        }
    
        return value > 0 && 
               value < 1_000_000_000 && 
               value % 1 == 0;
    }
}