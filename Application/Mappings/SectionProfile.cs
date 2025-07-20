using Application.Dto.Section;
using Application.Services;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class SectionProfile : Profile
{
    public SectionProfile()
    {
        var isAvailableExpr = MenuItemService.IsAvailable(DateTime.Now.TimeOfDay);

        CreateMap<Section, MenuSectionDto>()
            .ForMember(dest => dest.MenuItems, opt =>
                opt.MapFrom(src =>
                    src.MenuItems.AsQueryable().Where(isAvailableExpr)
                )
            );

        CreateMap<Section, SectionDto>();
        CreateMap<CreateSectionRequest, Section>();
        CreateMap<Section, SectionResponse>();
        CreateMap<UpdateSectionRequest, Section>();
    }
}