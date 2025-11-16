using Microsoft.AspNetCore.Mvc;
using Muno.Application.Dto.ActivityPeriod;
using Muno.Application.Dto.Shared;
using Muno.Application.Extensions;

namespace Muno.Application.Dto.Restaurant;

public class CreateRestaurantRequest : IHasTranslationsDto<RestaurantTranslationDto>
{
    public ActivityPeriodRequest ActivityPeriod { get; set; }
    
    public Dictionary<string, string> OpeningHours { get; set; }
    
    [ModelBinder(BinderType = typeof(JsonModelBinder))]
    public ICollection<RestaurantTranslationDto> Translations { get; set; }
}