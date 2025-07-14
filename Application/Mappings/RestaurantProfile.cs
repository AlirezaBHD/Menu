using Application.Dto.Restaurant;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class RestaurantProfile : Profile
{
    public RestaurantProfile()
    {
        CreateMap<Restaurant,RestaurantMenuDto>();
        CreateMap<CreateRestaurantRequest,Restaurant>();
        CreateMap<UpdateRestaurantRequest,Restaurant>();
        CreateMap<Restaurant,RestaurantResponse>();
    }
}