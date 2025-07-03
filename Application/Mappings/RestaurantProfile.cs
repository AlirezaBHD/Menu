using Application.Dto.Category;
using Application.Dto.Restaurant;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class RestaurantProfile : Profile
{
    public RestaurantProfile()
    {
        CreateMap<CreateRestaurantRequest,Restaurant>();
    }
}