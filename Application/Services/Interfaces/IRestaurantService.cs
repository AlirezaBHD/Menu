using Application.Dto.Restaurant;
using Domain.Entities;

namespace Application.Services.Interfaces;

public interface IRestaurantService : IService<Restaurant>
{
    Task CreateRestaurantAsync(CreateRestaurantRequest createRestaurantRequest);
    Task UpdateRestaurantAsync(Guid id, UpdateRestaurantRequest dto);
    Task<RestaurantResponse> GetRestaurantByIdAsync(Guid id);
}