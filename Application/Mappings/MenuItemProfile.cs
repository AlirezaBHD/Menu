using Application.Dto.MenuItem;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class MenuItemProfile : Profile
{
    public MenuItemProfile()
    {
        CreateMap<MenuItem, MenuItemDto>();
    }
}