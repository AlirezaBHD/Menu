using Application.Dto.MenuItem;
using Application.Dto.Shared;
using Domain.Entities;

namespace Application.Services.Interfaces;

public interface IMenuItemService: IService<MenuItem>
{
    Task<MenuItemResponse> CreateMenuItemAsync(Guid sectionId, CreateMenuItemRequest createMenuItemRequest);
    Task DeleteMenuItemAsync(Guid id);
    Task UpdateMenuItemAsync(Guid id, UpdateMenuItemRequest dto);
    Task<IEnumerable<MenuItemListResponse>> GetMenuItemListAsync();
    Task UpdateMenuItemOrderAsync(List<OrderDto> dto);
}