using Application.Dto.Section;
using Application.Services;
using AutoMapper;
using Domain.Entities.Sections;
using Domain.Interfaces.Services;
using Domain.Localization;

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
        CreateMap<Section, SectionListResponse>().ForMember(dest => dest.CategoryTitle, opt =>
            opt.MapFrom((src, dest, destMember, context) =>
            {
                var currentLanguage = context.Items["CurrentLanguage"] as ICurrentLanguage;
                var lang = currentLanguage?.GetLanguage() ?? SupportedLanguages.All[0].Code;

                return src.Category?.Translations
                           .FirstOrDefault(t => t.LanguageCode == lang)?.Title
                       ?? src.Category?.Translations.FirstOrDefault()?.Title
                       ?? string.Empty;
            }));
    }
}