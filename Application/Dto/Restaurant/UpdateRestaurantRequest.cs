using Application.Dto.Shared;
using Microsoft.AspNetCore.Http;

namespace Application.Dto.Restaurant;

public class UpdateRestaurantRequest : IHasTranslationsDto<RestaurantTranslationDto>
{
    // public IFormFile? LogoFile { get; set; }
    public Dictionary<string, string> OpeningHours { get; set; }

    public ICollection<RestaurantTranslationDto> Translations { get; set; } =
        new List<RestaurantTranslationDto>();
}