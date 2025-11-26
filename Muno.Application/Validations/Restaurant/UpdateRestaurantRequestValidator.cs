using FluentValidation;
using Muno.Application.Dto.Restaurant;

namespace Muno.Application.Validations.Restaurant;

public class UpdateRestaurantRequestValidator : AbstractValidator<UpdateRestaurantRequest>
{
    public UpdateRestaurantRequestValidator()
    {
        RuleForEach(x => x.Translations)
            .SetValidator(new RestaurantTranslationValidator());
    }
}