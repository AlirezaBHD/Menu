using Application.Dto.MenuItemVariant;
using FluentValidation;

namespace Application.Validations.MenuItemVariant;

public class MenuItemVariantDtoValidator : AbstractValidator<MenuItemVariantDto>
{
    public MenuItemVariantDtoValidator()
    {
        RuleForEach(x => x.Translations)
            .SetValidator(new MenuItemVariantTranslationValidator());
    }
}