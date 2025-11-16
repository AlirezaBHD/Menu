using Muno.Application.Extensions;
using FluentValidation;
using Muno.Application.Dto.Restaurant;

namespace Muno.Application.Validations.Restaurant;

public class CreateRestaurantRequestValidator : AbstractValidator<CreateRestaurantRequest>
{
    public CreateRestaurantRequestValidator()
    {
        // RuleFor(r => r.LogoFile).ImageFileRule();
        
        RuleForEach(x => x.Translations)
            .SetValidator(new RestaurantTranslationValidator());
    }
}