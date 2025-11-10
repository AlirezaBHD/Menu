using Application.Dto.MenuItem;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities.MenuItems;
using Domain.Interfaces.Services;
using Domain.Localization;

namespace Application.Mappings;

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