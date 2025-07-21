using Application.Dto.ActivityPeriod;
using Application.Dto.MenuItemVariant;
using Application.Extensions;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Dto.MenuItem;

public class CreateMenuItemRequest
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public IFormFile ImageFile { get; set; }
    public bool IsAvailable { get; set; }
    public ActivityPeriodRequest ActivityPeriod { get; set; }
    
    [ModelBinder(BinderType = typeof(JsonModelBinder))]
    public List<MenuItemVariantDto> Variants { get; set; }
}