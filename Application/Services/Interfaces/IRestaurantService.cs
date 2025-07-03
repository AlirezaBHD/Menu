using Application.Dto.Restaurant;
using Domain.Entities;

namespace Application.Services.Interfaces;

public interface IRestaurantService : IService<Restaurant>
{
    Task CreateRestaurantAsync(CreateRestaurantRequest createRestaurantRequest);
}