using Application.Dto.Restaurant;
using Application.Exceptions;
using Application.Services;
using AutoMapper;
using Domain.Entities.Restaurants;

namespace Application.Mappings;

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

        CreateMap<RestaurantTranslationDto, RestaurantTranslation>();
        CreateMap<RestaurantTranslation, RestaurantTranslationDto>();
    }
}