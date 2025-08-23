using Application.Dto.Restaurant;
using Application.Extensions;
using FluentValidation;

namespace Application.Validations.Restaurant;

public class CreateRestaurantRequestValidator : AbstractValidator<CreateRestaurantRequest>
{
    public CreateRestaurantRequestValidator()
    {
        var entityType = typeof(Domain.Entities.MenuItem.MenuItem);

        RuleFor(r => r.Name)!
            .LengthValidationRule(dto => dto.Name, entityType);

        RuleFor(r => r.Description)!
            .LengthValidationRule(dto => dto.Description!, entityType, blank: true);

        RuleFor(r => r.Address)!
            .LengthValidationRule(dto => dto.Address, entityType);

        RuleFor(r => r.LogoFile).ImageFileRule();
    }
}