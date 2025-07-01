using Application.Dto.Category;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.RepositoryInterfaces;

namespace Application.Services;

public class MenuItemService : Service<MenuItem>, IMenuItemService 
{
    #region Injection
    
    private readonly ICategoryService _categoryService;

    public MenuItemService(IMapper mapper,IMenuItemRepository menuItemRepository, ICategoryService categoryService)
        : base(mapper, menuItemRepository)
    {
        _categoryService = categoryService;
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

}
