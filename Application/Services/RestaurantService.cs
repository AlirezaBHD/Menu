using Application.Dto.Restaurant;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class RestaurantService : Service<Restaurant>, IRestaurantService
{
    #region Injection

    private readonly IFileService _fileService;

    public RestaurantService(IRestaurantRepository restaurantRepository, IMapper mapper, IFileService fileService
        , ILogger<Restaurant> logger)
        : base(mapper, restaurantRepository, logger)
    {
        _fileService = fileService;
    }

    #endregion

    public async Task CreateRestaurantAsync(CreateRestaurantRequest createRestaurantRequest)
    {
        var entity = Mapper.Map<CreateRestaurantRequest, Restaurant>(createRestaurantRequest);
        await Repository.AddAsync(entity);
        await Repository.SaveAsync();
    }


    public async Task UpdateRestaurantAsync(Guid id, UpdateRestaurantRequest dto)
    {
        var restaurant = await Repository.GetByIdAsync(id);
        restaurant = Mapper.Map(dto, restaurant);

        if (dto.LogoFile != null)
        {
            var imagePath = await _fileService.SaveFileAsync(dto.LogoFile, "restaurant-logos");
            restaurant.LogoPath = imagePath;
        }

        Repository.Update(restaurant);
        await Repository.SaveAsync();
    }

    public async Task<RestaurantResponse> GetRestaurantByIdAsync(Guid id)
    {
        var result =
            await GetByIdProjectedAsync<RestaurantResponse>(id, trackingBehavior: TrackingBehavior.AsNoTracking);
        return result;
    }
}