using Application.Dto.Category;
using Application.Dto.MenuItem;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.RepositoryInterfaces;

namespace Application.Services;

public class MenuItemService : Service<MenuItem>, IMenuItemService
{
    #region Injection

    private readonly ICategoryService _categoryService;
    private readonly IFileService _fileService;

    public MenuItemService(IMapper mapper, IMenuItemRepository menuItemRepository, ICategoryService categoryService,
        IFileService fileService)
        : base(mapper, menuItemRepository)
    {
        _categoryService = categoryService;
        _fileService = fileService;
    }

    #endregion

    #region Get Restaurant Menu Async

    public async Task<IEnumerable<RestaurantMenuDto>> GetRestaurantMenuAsync(Guid restaurantId)
    {
        var result = await _categoryService.GetAllProjectedAsync<RestaurantMenuDto>(
            predicate: c => c.RestaurantId == restaurantId);
        return result;
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
        return response;
    }

    public async Task DeleteMenuItemAsync(Guid id)
    {
        var section = await Repository.GetByIdAsync(id);
        Repository.Remove(section);
        await Repository.SaveAsync();
    }

    public async Task UpdateMenuItemAsync(Guid id, UpdateMenuItemRequest dto)
    {
        var menuItem = await Repository.GetByIdAsync(id);
        menuItem = Mapper.Map(dto, menuItem);

        if (dto.ImageFile != null)
        {
            var imagePath = await _fileService.SaveFileAsync(dto.ImageFile, "MenuItem");
            menuItem.ImagePath = imagePath;
        }

        Repository.Update(menuItem);
        await Repository.SaveAsync();    
    }
}