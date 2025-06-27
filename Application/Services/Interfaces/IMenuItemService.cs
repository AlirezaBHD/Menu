using Application.Dto.Category;
using Domain.Entities;

namespace Application.Services.Interfaces;

public interface IMenuItemService: IService<MenuItem>
{
    Task<IEnumerable<CategoryDto>> GetMenuItemsAsync(Guid restaurantId);
}