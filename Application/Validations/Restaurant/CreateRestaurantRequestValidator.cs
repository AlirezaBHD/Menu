using Application.Dto.Restaurant;
using Application.Extensions;
using FluentValidation;

namespace Application.Validations.Restaurant;

public class CreateRestaurantRequestValidator : AbstractValidator<CreateRestaurantRequest>
{
    public CreateRestaurantRequestValidator()
    {
        // RuleFor(r => r.LogoFile).ImageFileRule();
        
        RuleForEach(x => x.Translations)
            .SetValidator(new RestaurantTranslationValidator());
    }
}