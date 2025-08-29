using Application.Dto.MenuItem;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.MenuItem;

namespace Application.Mappings;

public class MenuItemProfile : Profile
{
    public MenuItemProfile()
    {
        CreateMap<MenuItem, MenuItemDto>()
            .ForAllMultiLanguageMembers();
        
        CreateMap<MenuItem, MenuItemResponse>();
        CreateMap<CreateMenuItemRequest, MenuItem>();
        CreateMap<UpdateMenuItemRequest, MenuItem>();
        CreateMap<MenuItem, MenuItemListResponse>()
            .ForMember(dest => dest.SectionTitle, opt =>
            opt.MapFrom(src =>
                src.Section!.Title)); 
    }
}