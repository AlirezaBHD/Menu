using Application.Dto.ActivityPeriod;
using Application.Dto.MenuItemVariant;
using Application.Dto.Shared;
using Application.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Dto.MenuItem;

public class UpdateMenuItemRequest: IHasTranslationsDto<MenuItemTranslationDto>
{
    public bool IsAvailable { get; set; }
    public ActivityPeriodRequest ActivityPeriod { get; set; }
    
    [ModelBinder(BinderType = typeof(JsonModelBinder))]
    public List<MenuItemVariantDto> Variants { get; set; }

    [ModelBinder(BinderType = typeof(JsonModelBinder))]
    public ICollection<MenuItemTranslationDto> Translations { get; set; } = new List<MenuItemTranslationDto>();
}