using Application.Dto.MenuItem;
using Application.Extensions;
using Domain.Entities.MenuItems;
using FluentValidation;

namespace Application.Validations.MenuItem;

public class MenuItemTranslationValidator : AbstractValidator<MenuItemTranslationDto>
{
    public MenuItemTranslationValidator()
    {
        var entityType = typeof(MenuItemTranslation);

        RuleFor(c => c.Title)!
            .LengthValidationRule(dto => dto.Title!, entityType);
        
        RuleFor(c => c.Description)!
            .LengthValidationRule(dto => dto.Description!, entityType, blank: true);
    }
}