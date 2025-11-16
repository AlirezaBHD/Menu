using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Muno.Application.Dto.Shared;
using Muno.Application.Extensions;

namespace Muno.Application.Dto.Restaurant;

public class UpdateRestaurantRequest : IHasTranslationsDto<RestaurantTranslationDto>
{
    public IFormFile? LogoFile { get; set; }
    public Dictionary<string, string> OpeningHours { get; set; }

    [ModelBinder(BinderType = typeof(JsonModelBinder))]
    public ICollection<RestaurantTranslationDto> Translations { get; set; } =
        new List<RestaurantTranslationDto>();
}