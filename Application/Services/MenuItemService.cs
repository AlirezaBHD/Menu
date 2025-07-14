using Application.Dto.MenuItem;
using Application.Dto.Restaurant;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
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
}