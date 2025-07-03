using Application.Dto.Restaurant;
using Application.Extensions;
using FluentValidation;

namespace Application.Validations.Restaurant;

public class UpdateRestaurantRequestValidator : AbstractValidator<UpdateRestaurantRequest>
{
    public UpdateRestaurantRequestValidator()
    {
        var entityType = typeof(Domain.Entities.MenuItem);

        RuleFor(r => r.Name)!
            .ApplyLengthValidation(dto => dto.Name, entityType);

        RuleFor(r => r.Description)!
            .ApplyLengthValidation(dto => dto.Description!, entityType, blank: true);

        RuleFor(r => r.Address)!
            .ApplyLengthValidation(dto => dto.Address, entityType);

        RuleFor(r => r.LogoFile)!.ImageRule(blank: true);
    }
}