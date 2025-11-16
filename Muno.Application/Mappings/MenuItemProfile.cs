using Muno.Application.Exceptions;
using AutoMapper;
using Muno.Domain.Entities.MenuItems;
using Muno.Domain.Interfaces.Services;
using Muno.Domain.Localization;
using Muno.Application.Dto.MenuItem;

namespace Muno.Application.Mappings;

public class MenuItemProfile : Profile
{
    public MenuItemProfile()
    {
        CreateMap<MenuItem, MenuItemDto>()
            .ForAllMultiLanguageMembers();
        
        CreateMap<MenuItem, ItemMenuDto>()
            .ForAllMultiLanguageMembers();
        
        CreateMap<MenuItem, MenuItemResponse>().ForAllMultiLanguageMembers();
        CreateMap<CreateMenuItemRequest, MenuItem>();
        CreateMap<UpdateMenuItemRequest, MenuItem>();
        CreateMap<MenuItem, MenuItemListResponse>().ForAllMultiLanguageMembers();
        CreateMap<MenuItemTranslation, MenuItemTranslationDto>();
        CreateMap<MenuItemTranslationDto, MenuItemTranslation>();
    }
}