using Application.Services.Interfaces;
using AutoMapper;
using Domain.RepositoryInterfaces;

namespace Application.Services;


public class RestaurantService : IRestaurantService
{
    #region Injection

    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IMapper _mapper;

    public RestaurantService(IRestaurantRepository restaurantRepository, IMapper mapper)
    {
        _restaurantRepository = restaurantRepository;
        _mapper = mapper;
    }

    #endregion
}
