using Application.Dto.Section;
using Application.Exceptions;
using Application.Services;
using AutoMapper;
using Domain.Entities.Sections;

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