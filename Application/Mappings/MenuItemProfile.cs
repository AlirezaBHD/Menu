using Application.Dto.MenuItem;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.MenuItems;
using Domain.Interfaces.Services;
using Domain.Localization;

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
        CreateMap<MenuItem, MenuItemListResponse>().ForMember(dest => dest.SectionTitle, opt =>
            opt.MapFrom((src, dest, destMember, context) =>
            {
                var currentLanguage = context.Items["CurrentLanguage"] as ICurrentLanguage;
                var lang = currentLanguage?.GetLanguage() ?? SupportedLanguages.All[0].Code;

                return src.Section?.Translations
                           .FirstOrDefault(t => t.LanguageCode == lang)?.Title
                       ?? src.Section?.Translations.FirstOrDefault()?.Title
                       ?? string.Empty;
            }));
    }
}