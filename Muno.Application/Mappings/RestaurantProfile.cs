using Muno.Application.Exceptions;
using AutoMapper;
using Domain.Entities.Restaurants;
using Muno.Application.Dto.Restaurant;
using Muno.Application.Services;

namespace Muno.Application.Mappings;

public class RestaurantProfile : Profile
{
    public RestaurantProfile()
    {
        var isAvailableExpr = CategoryService.IsAvailable(DateTime.Now.TimeOfDay);

        CreateMap<Restaurant, RestaurantMenuDto>()
            .ForAllMultiLanguageMembers()
            .ForMember(dest => dest.Categories, opt =>
                opt.MapFrom(src =>
                    src.Categories.AsQueryable().Where(isAvailableExpr)
                )
            );

        CreateMap<CreateRestaurantRequest, Restaurant>();
        CreateMap<UpdateRestaurantRequest, Restaurant>();
        CreateMap<Restaurant, RestaurantResponse>().ForAllMultiLanguageMembers();
        CreateMap<Restaurant, RestaurantDto>().ForAllMultiLanguageMembers();
        CreateMap<Restaurant, RestaurantDetailDto>().ForAllMultiLanguageMembers();
        CreateMap<RestaurantTranslationDto, RestaurantTranslation>();
        CreateMap<RestaurantTranslation, RestaurantTranslationDto>();
    }
}