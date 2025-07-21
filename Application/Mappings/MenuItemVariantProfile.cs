using Application.Dto.MenuItemVariant;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class MenuItemVariantProfile : Profile
{
    public MenuItemVariantProfile()
    {
        CreateMap<MenuItemVariantDto,MenuItemVariant>();
        CreateMap<MenuItemVariant,MenuItemVariantDto>();
    }
}
