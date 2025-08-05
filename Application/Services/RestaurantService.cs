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
        Logger.LogInformation("Created new restaurant: {@Restaurant}", entity);
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
        Logger.LogInformation("Updated restaurant with ID: {Id}. Data: {@UpdateData}", id, restaurant);
    }

    public async Task<RestaurantResponse> GetRestaurantByIdAsync(Guid id)
    {
        var result =
            await GetByIdProjectedAsync<RestaurantResponse>(id, trackingBehavior: TrackingBehavior.AsNoTracking);
        return result;
    }

    #region Get Restaurant Menu Async

    public async Task<IEnumerable<RestaurantMenuDto>> GetRestaurantMenuAsync(Guid restaurantId)
    {
        var query = Repository.GetQueryable();
        var result = await GetAllProjectedAsync<RestaurantMenuDto>(
            query:query,
            predicate: r => r.Id == restaurantId);
        return result;
    }

    #endregion
}