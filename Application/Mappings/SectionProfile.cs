using Application.Dto.Section;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class SectionProfile : Profile
{
    public SectionProfile()
    {
        CreateMap<Section, SectionDto>()
            .ForMember(dest => dest.MenuItems, opt =>
            opt.MapFrom(src => src.MenuItems
                .Where(mi => mi.IsAvailable)));
    }
}