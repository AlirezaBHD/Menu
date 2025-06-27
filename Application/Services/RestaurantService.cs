using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.RepositoryInterfaces;

namespace Application.Services;


public class RestaurantService :Service<Restaurant>, IRestaurantService
{
    #region Injection

    public RestaurantService(IRestaurantRepository restaurantRepository, IMapper mapper) 
        : base(mapper, restaurantRepository)
    {
    }

    #endregion
}
