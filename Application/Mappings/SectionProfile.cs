using Application.Dto.Section;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class SectionProfile : Profile
{
    public SectionProfile()
    {
        CreateMap<Section, AvailableMenuItemSectionDto>()
            .ForMember(dest => dest.MenuItems, opt =>
            opt.MapFrom(src => src.MenuItems
                .Where(mi => mi.IsAvailable)));

        CreateMap<Section, SectionDto>();
        CreateMap<CreateSectionRequest, Section>();
        CreateMap<Section, SectionResponse>();
        CreateMap<UpdateSectionRequest, Section>();
    }
}