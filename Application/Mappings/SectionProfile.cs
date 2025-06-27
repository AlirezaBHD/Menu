using Application.Dto.Section;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class SectionProfile : Profile
{
    public SectionProfile()
    {
        CreateMap<Section, SectionDto>();
    }
}