using Muno.Domain.Entities.Restaurants;
using Muno.Application.Dto.Restaurant;
using Muno.Application.Dto.Shared;

namespace Muno.Application.Services.Interfaces;

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