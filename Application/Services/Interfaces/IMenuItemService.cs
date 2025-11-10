using Application.Dto.MenuItem;
using Application.Dto.Shared;
using Domain.Entities;
using Domain.Entities.MenuItems;

namespace Application.Services.Interfaces;

public interface IMenuItemService: IService<MenuItem>
{
    Task<MenuItemResponse> CreateMenuItemAsync(int sectionId, CreateMenuItemRequest createMenuItemRequest);
    Task DeleteMenuItemAsync(int id);
    Task UpdateMenuItemAsync(int id, UpdateMenuItemRequest dto);
    Task<IEnumerable<MenuItemListResponse>> GetMenuItemListAsync();
    Task UpdateMenuItemOrderAsync(List<OrderDto> dto);
    Task<MenuItemDto> GetMenuItemByIdAsync(int id);
    Task<string> EditImageAsync(int menuItemId, ImageDto image);
}