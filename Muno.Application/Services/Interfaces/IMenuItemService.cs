using Muno.Domain.Entities;
using Muno.Domain.Entities.MenuItems;
using Muno.Application.Dto.MenuItem;
using Muno.Application.Dto.Shared;

namespace Muno.Application.Services.Interfaces;

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