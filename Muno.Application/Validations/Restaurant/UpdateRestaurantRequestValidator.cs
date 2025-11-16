using Muno.Application.Extensions;
using FluentValidation;
using Muno.Application.Dto.Restaurant;

namespace Muno.Application.Validations.Restaurant;

public class UpdateRestaurantRequestValidator : AbstractValidator<UpdateRestaurantRequest>
{
    public UpdateRestaurantRequestValidator()
    {
        // RuleFor(r => r.LogoFile)!.ImageFileRule(blank: true);
        
        RuleForEach(x => x.Translations)
            .SetValidator(new RestaurantTranslationValidator());
    }
}