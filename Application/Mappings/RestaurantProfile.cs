using Application.Dto.Restaurant;
using Application.Extensions;
using Application.Services;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class RestaurantProfile : Profile
{
    public RestaurantProfile()
    {
        var isAvailableExpr = CategoryService.IsAvailable(DateTime.Now.TimeOfDay);

        CreateMap<Restaurant, RestaurantMenuDto>()
            .ForMember(dest => dest.Categories, opt =>
                opt.MapFrom(src =>
                    src.Categories.AsQueryable().Where(isAvailableExpr)
                )
            );
        
        CreateMap<CreateRestaurantRequest,Restaurant>();
        CreateMap<UpdateRestaurantRequest,Restaurant>();
        CreateMap<Restaurant,RestaurantResponse>();
    }
}
