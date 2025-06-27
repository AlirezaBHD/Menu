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
    
    private readonly ICategoryRepository _categoryRepository;

    public MenuItemService(IMapper mapper,IMenuItemRepository menuItemRepository, ICategoryRepository categoryRepository)
        : base(mapper, menuItemRepository)
    {
        _categoryRepository = categoryRepository;
    }

    #endregion

    #region Get Menu Items

    public async Task<IEnumerable<CategoryDto>> GetMenuItemsAsync()
    {
        var queryable = _categoryRepository.GetQueryable();
        var query = queryable.Include(c => c.Sections).ThenInclude(s => s.MenuItems)
;
        var result = await query.ProjectTo<CategoryDto>(Mapper.ConfigurationProvider).ToListAsync();
        return result;
    }

    #endregion

}
