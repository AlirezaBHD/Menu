using Muno.Application.Exceptions;
using AutoMapper;
using Domain.Entities.Sections;
using Muno.Application.Dto.Section;
using Muno.Application.Services;

namespace Muno.Application.Mappings;

public class SectionProfile : Profile
{
    public SectionProfile()
    {
        var isAvailableExpr = MenuItemService.IsAvailable(DateTime.Now.TimeOfDay);

        CreateMap<Section, MenuSectionDto>()
            .ForAllMultiLanguageMembers()
            .ForMember(dest => dest.MenuItems, opt =>
                opt.MapFrom(src =>
                    src.MenuItems.AsQueryable().Where(isAvailableExpr)
                )
            );

        CreateMap<Section, SectionDto>().ForAllMultiLanguageMembers();
        CreateMap<CreateSectionRequest, Section>();
        CreateMap<Section, SectionResponse>();
        CreateMap<UpdateSectionRequest, Section>();


        CreateMap<Section, SectionListResponse>()
            .ForAllMultiLanguageMembers();

        CreateMap<SectionTranslationDto, SectionTranslation>();
        CreateMap<SectionTranslation, SectionTranslationDto>();
    }
}