using Application.Dto.Restaurant;
using Application.Extensions;
using FluentValidation;

namespace Application.Validations.Restaurant;

public class UpdateRestaurantRequestValidator : AbstractValidator<UpdateRestaurantRequest>
{
    public UpdateRestaurantRequestValidator()
    {
        // RuleFor(r => r.LogoFile)!.ImageFileRule(blank: true);
        
        RuleForEach(x => x.Translations)
            .SetValidator(new RestaurantTranslationValidator());
    }
}