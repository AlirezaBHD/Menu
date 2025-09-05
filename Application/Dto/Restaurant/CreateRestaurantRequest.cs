using Application.Dto.Shared;
using Application.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Dto.Restaurant;

public class CreateRestaurantRequest : IHasTranslationsDto<RestaurantTranslationDto>
{
    public int OwnerId { get; set; }
    public IFormFile LogoFile { get; set; }
    
    public Dictionary<string, string> OpeningHours { get; set; }
    
    [ModelBinder(BinderType = typeof(JsonModelBinder))]
    public ICollection<RestaurantTranslationDto> Translations { get; set; }
}