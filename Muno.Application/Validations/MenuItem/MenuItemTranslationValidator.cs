using Muno.Application.Extensions;
using Muno.Domain.Entities.MenuItems;
using FluentValidation;
using Muno.Application.Dto.MenuItem;

namespace Muno.Application.Validations.MenuItem;

public class MenuItemTranslationValidator : AbstractValidator<MenuItemTranslationDto>
{
    public MenuItemTranslationValidator()
    {
        var entityType = typeof(MenuItemTranslation);

        RuleFor(c => c.Title)!
            .LengthValidationRule(dto => dto.Title, entityType);
        
        RuleFor(c => c.Description)!
            .LengthValidationRule(dto => dto.Description!, entityType, blank: true);
    }
}