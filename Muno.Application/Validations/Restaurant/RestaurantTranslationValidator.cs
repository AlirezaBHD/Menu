using Muno.Application.Extensions;
using Domain.Entities.Restaurants;
using FluentValidation;
using Muno.Application.Dto.Restaurant;

namespace Muno.Application.Validations.Restaurant;


public class RestaurantTranslationValidator : AbstractValidator<RestaurantTranslationDto>
{
    public RestaurantTranslationValidator()
    {
        var entityType = typeof(RestaurantTranslation);
        
        RuleFor(r => r.Name)!
            .LengthValidationRule(dto => dto.Name, entityType);
        
        RuleFor(r => r.Description)!
            .LengthValidationRule(dto => dto.Description!, entityType, blank: true);
        
        RuleFor(r => r.Address)!
            .LengthValidationRule(dto => dto.Address, entityType); //TODO
    }
}