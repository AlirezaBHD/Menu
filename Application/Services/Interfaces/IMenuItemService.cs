using Application.Dto.Category;
using Application.Dto.MenuItem;
using Domain.Entities;

namespace Application.Services.Interfaces;

public interface IMenuItemService: IService<MenuItem>
{
    Task<IEnumerable<RestaurantMenuDto>> GetRestaurantMenuAsync(Guid restaurantId);
    Task<MenuItemResponse> CreateMenuItemAsync(Guid sectionId, CreateMenuItemRequest createMenuItemRequest);
    Task DeleteMenuItemAsync(Guid id);
}