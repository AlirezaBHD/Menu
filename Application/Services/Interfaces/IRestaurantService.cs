using Application.Dto.Restaurant;
using Application.Dto.Shared;
using Domain.Entities.Restaurants;

namespace Application.Services.Interfaces;

public interface IRestaurantService : IService<Restaurant>
{
    Task<ResponseDto> CreateRestaurantAsync(CreateRestaurantRequest createRestaurantRequest);
    Task UpdateRestaurantAsync(int id, UpdateRestaurantRequest dto);
    Task<RestaurantResponse> GetRestaurantByIdAsync(int id);
    Task<RestaurantMenuDto> GetRestaurantMenuAsync(int restaurantId);
    Task<string> EditImageAsync(int id, ImageDto image);
    Task UpdateRestaurantOrderAsync(List<OrderDto> dto);
    Task<IEnumerable<RestaurantDto>> RestaurantDetailList();
    Task<RestaurantDetailDto> RestaurantDetail(int id);
}