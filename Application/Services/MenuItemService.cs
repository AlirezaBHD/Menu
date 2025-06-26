using Application.Services.Interfaces;
using AutoMapper;
using Domain.RepositoryInterfaces;

namespace Application.Services;

public class MenuItemService : IMenuItemService
{
    #region Injection

    private readonly IMenuItemRepository _menuItemRepository;
    private readonly IMapper _mapper;

    public MenuItemService(IMenuItemRepository menuItemRepository, IMapper mapper)
    {
        _menuItemRepository = menuItemRepository;
        _mapper = mapper;
    }

    #endregion
}
