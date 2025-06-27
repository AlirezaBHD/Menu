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
        _menuItemRepository = menuItemRepository;
        _mapper = mapper;
    }

    #endregion

}
