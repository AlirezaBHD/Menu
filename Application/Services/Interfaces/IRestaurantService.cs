using Application.Dto.Restaurant;
using Domain.Entities.Restaurants;

namespace Application.Services.Interfaces;

public interface IRestaurantService : IService<Restaurant>
{
    Task CreateRestaurantAsync(CreateRestaurantRequest createRestaurantRequest);
    Task UpdateRestaurantAsync(int id, UpdateRestaurantRequest dto);
    Task<RestaurantResponse> GetRestaurantByIdAsync(int id);
    Task<IEnumerable<RestaurantMenuDto>> GetRestaurantMenuAsync(int restaurantId);
}