using Application.Dto.Category;
using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

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

    #region Get Menu Items

    public async Task<IEnumerable<CategoryDto>> GetMenuItemsAsync()
    {
        var result = await _categoryService.GetAllProjectedAsync<CategoryDto>();
        return result;
    }

    #endregion

}
