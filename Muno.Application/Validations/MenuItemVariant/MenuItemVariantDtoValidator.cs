using FluentValidation;
using Muno.Application.Dto.MenuItemVariant;

namespace Muno.Application.Validations.MenuItemVariant;

public class MenuItemVariantDtoValidator : AbstractValidator<MenuItemVariantDto>
{
    public MenuItemVariantDtoValidator()
    {
        RuleForEach(x => x.Translations)
            .SetValidator(new MenuItemVariantTranslationValidator());
    }
}