using Muno.Application.Exceptions;
using AutoMapper;
using Domain.Entities.MenuItemVariants;
using Muno.Application.Dto.MenuItemVariant;

namespace Muno.Application.Mappings;

public class MenuItemVariantProfile : Profile
{
    public MenuItemVariantProfile()
    {
        CreateMap<MenuItemVariantDto,MenuItemVariant>();
        CreateMap<MenuItemVariant,MenuItemVariantDto>();
        CreateMap<MenuItemVariantTranslationDto,MenuItemVariantTranslation>();
        CreateMap<MenuItemVariantTranslation,MenuItemVariantTranslationDto>();
        CreateMap<MenuItemVariant,MenuItemVariantResponseDto>().ForAllMultiLanguageMembers();
        CreateMap<CreateMenuItemVariantDto,MenuItemVariant>();
    }
}
