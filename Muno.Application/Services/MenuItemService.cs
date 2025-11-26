using System.Linq.Expressions;
using AutoMapper;
using Muno.Domain.Entities;
using Muno.Domain.Entities.MenuItems;
using Muno.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Muno.Application.Dto.MenuItem;
using Muno.Application.Dto.Shared;
using Muno.Application.Exceptions;
using Muno.Application.Localization;
using Muno.Application.Services.Interfaces;

namespace Muno.Application.Services;

public class MenuItemService(
    IMapper mapper,
    IMenuItemRepository menuItemRepository,
    IFileService fileService,
    ILogger<MenuItem> logger)
    : Service<MenuItem>(mapper, menuItemRepository, logger), IMenuItemService
{

    public static Expression<Func<MenuItem, bool>> IsAvailable(TimeSpan nowTime)
    {
        return mi =>
            mi.ActivityPeriod.IsActive && (
                mi.ActivityPeriod.ActivityType == ActivityEnum.Unlimited ||
                (
                    (mi.ActivityPeriod.ActivityType == ActivityEnum.ActivePeriod &&
                     nowTime >= mi.ActivityPeriod.FromTime &&
                     nowTime <= mi.ActivityPeriod.ToTime)
                    ||
                    (mi.ActivityPeriod.ActivityType == ActivityEnum.InactivePeriod &&
                     (nowTime < mi.ActivityPeriod.FromTime ||
                      nowTime > mi.ActivityPeriod.ToTime))
                ));
    }


    public async Task<MenuItemResponse> CreateMenuItemAsync(int sectionId, CreateMenuItemRequest createMenuItemRequest)
    {
        var entity = Mapper.Map<CreateMenuItemRequest, MenuItem>(createMenuItemRequest);

        entity.SectionId = sectionId;

        var count = Queryable.Count();
        entity.Order = count + 1;
        
        await Repository.AddAsync(entity);
        await Repository.SaveAsync();
        var response = Mapper.Map<MenuItem, MenuItemResponse>(entity);
        Logger.LogInformation("Created new menu item in section {SectionId}. Data: {@UpdateData}", sectionId, entity);
        return response;
    }


    public async Task DeleteMenuItemAsync(int id)
    {
        var section = await Repository.GetByIdAsync(id);
        Repository.Remove(section);
        await Repository.SaveAsync();
        Logger.LogInformation("Deleted menu item with ID: {Id}", id);
    }


    public async Task UpdateMenuItemAsync(int id, UpdateMenuItemRequest dto)
    {
        var menuItem = await Queryable
            .Include(mi => mi.Translations)
            .Include(mi => mi.Variants)
            .FirstAsync(c => c.Id == id);

        menuItem = Mapper.Map(dto, menuItem);

        Repository.Update(menuItem);
        await Repository.SaveAsync();
        Logger.LogInformation("Updated menu item with ID {Id}. Data: {@UpdateData}", id, menuItem);
    }


    public async Task<IEnumerable<MenuItemListResponse>> GetMenuItemListAsync()
    {
        var result = await GetAllProjectedAsync<MenuItemListResponse>(
            includes: [m => m.Translations],
            trackingBehavior: TrackingBehavior.AsNoTrackingWithIdentityResolution);
        return result.OrderBy(s => s.Order);
    }
    

    public async Task UpdateMenuItemOrderAsync(List<OrderDto> dto)
    {
        var allMenuItemCount = Queryable.Count();
        if (allMenuItemCount != dto.Count)
            throw new ValidationException(Resources.WrongNumberOfObjects);

        var orderMap = dto.ToDictionary(d => d.Id, d => d.Order);

        var menuItemIds = orderMap.Keys.ToList();
        var menuItems = await Queryable
            .Where(c => menuItemIds.Contains(c.Id))
            .ToListAsync();

        foreach (var section in menuItems)
        {
            if (!orderMap.TryGetValue(section.Id, out var newOrder)) continue;
            if (section.Order != newOrder)
            {
                section.Order = newOrder;
            }
        }

        await Repository.SaveAsync();
    }


    public async Task<MenuItemDto> GetMenuItemByIdAsync(int id)
    {

        var query = Queryable.Include(s => s.Translations)
            .Include(s => s.Variants).ThenInclude(m => m.Translations);

        var response =
            await GetByIdProjectedAsync<MenuItemDto>(id,
                query: query,
                trackingBehavior: TrackingBehavior.AsNoTracking);

        return response;
    }
    

    public async Task<string> EditImageAsync(int menuItemId, ImageDto image)
    {
        var menuItem = await Queryable.FirstOrDefaultAsync(i => i.Id == menuItemId);
        
        if (menuItem == null)
            throw new ValidationException(Resources.NotFound);
        
        var imagePath = await fileService.SaveFileAsync(image.File, "menu-item");
        
        menuItem.ImagePath = imagePath;
        await Repository.SaveAsync();
        
        return imagePath;
    }
}