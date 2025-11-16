using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Muno.Application.Dto.ActivityPeriod;
using Muno.Application.Dto.MenuItemVariant;
using Muno.Application.Dto.Shared;
using Muno.Application.Extensions;

namespace Muno.Application.Dto.MenuItem;

public class UpdateMenuItemRequest: IHasTranslationsDto<MenuItemTranslationDto>
{
    public bool IsAvailable { get; set; }
    public ActivityPeriodRequest ActivityPeriod { get; set; }
    
    [ModelBinder(BinderType = typeof(JsonModelBinder))]
    public List<MenuItemVariantDto> Variants { get; set; }

    [ModelBinder(BinderType = typeof(JsonModelBinder))]
    public ICollection<MenuItemTranslationDto> Translations { get; set; } = new List<MenuItemTranslationDto>();
}