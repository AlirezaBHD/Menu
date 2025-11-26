using FluentValidation;
using Muno.Application.Dto.Restaurant;

namespace Muno.Application.Validations.Restaurant;

public class CreateRestaurantRequestValidator : AbstractValidator<CreateRestaurantRequest>
{
    public CreateRestaurantRequestValidator()
    {
        RuleForEach(x => x.Translations)
            .SetValidator(new RestaurantTranslationValidator());
    }
}