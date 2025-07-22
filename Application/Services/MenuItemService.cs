using System.Linq.Expressions;
using Application.Dto.MenuItem;
using Application.Dto.Shared;
using Application.Exceptions;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class MenuItemService : Service<MenuItem>, IMenuItemService
{
    #region Injection

    private readonly IFileService _fileService;

    public MenuItemService(IMapper mapper, IMenuItemRepository menuItemRepository,
        IFileService fileService, ILogger<MenuItem> logger)
        : base(mapper, menuItemRepository, logger)
    {
        _fileService = fileService;
    }

    #endregion
    
    #region Activity Expression

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

    #endregion
    
    public async Task<MenuItemResponse> CreateMenuItemAsync(Guid sectionId, CreateMenuItemRequest createMenuItemRequest)
    {
        var entity = Mapper.Map<CreateMenuItemRequest, MenuItem>(createMenuItemRequest);
        entity.SectionId = sectionId;
        var imagePath = await _fileService.SaveFileAsync(createMenuItemRequest.ImageFile, "MenuItem");
        entity.ImagePath = imagePath;
        await Repository.AddAsync(entity);
        await Repository.SaveAsync();
        var response = Mapper.Map<MenuItem, MenuItemResponse>(entity);
        Logger.LogInformation("Created new menu item in section {SectionId}. Data: {@UpdateData}", sectionId, entity);
        return response;
    }

    public async Task DeleteMenuItemAsync(Guid id)
    {
        var section = await Repository.GetByIdAsync(id);
        Repository.Remove(section);
        await Repository.SaveAsync();
        Logger.LogInformation("Deleted menu item with ID: {Id}", id);
    }

    public async Task UpdateMenuItemAsync(Guid id, UpdateMenuItemRequest dto)
    {
        var menuItem = await Repository.GetByIdAsync(id);
        menuItem = Mapper.Map(dto, menuItem);

        if (dto.ImageFile != null)
        {
            var imagePath = await _fileService.SaveFileAsync(dto.ImageFile, "menu-item");
            menuItem.ImagePath = imagePath;
        }

        Repository.Update(menuItem);
        await Repository.SaveAsync();
        Logger.LogInformation("Updated menu item with ID {Id}. Data: {@UpdateData}", id, menuItem);
    }
    
    public async Task<IEnumerable<MenuItemListResponse>> GetSectionListAsync()
    {
        var result =await GetAllProjectedAsync<MenuItemListResponse>(trackingBehavior:TrackingBehavior.AsNoTracking);
        return  result.OrderBy(s => s.Order);
    }

    public async Task UpdateSectionOrderAsync(List<OrderDto> dto)
    {
        var allMenuItemCount = Queryable.Count();
        if (allMenuItemCount != dto.Count)
            throw new ValidationException("تعداد آبجکت های ورودی با تعداد آبجکت های موجود مغایرت دارد");
        
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
}