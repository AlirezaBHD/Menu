using AutoMapper;
using Muno.Domain.Entities.Restaurants;
using Muno.Domain.Interfaces.Repositories;
using Muno.Domain.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Muno.Application.Dto.Restaurant;
using Muno.Application.Dto.Shared;
using Muno.Application.Exceptions;
using Muno.Application.Localization;
using Muno.Application.Services.Interfaces;

namespace Muno.Application.Services;

public class RestaurantService : Service<Restaurant>, IRestaurantService
{
    #region Injection

    private readonly IFileService _fileService;
    private readonly ICurrentUser _currentUser;

    public RestaurantService(IRestaurantRepository restaurantRepository, IMapper mapper, IFileService fileService
        , ILogger<Restaurant> logger, ICurrentUser currentUser)
        : base(mapper, restaurantRepository, logger)
    {
        _fileService = fileService;
        _currentUser = currentUser;
    }

    #endregion

    public async Task<ResponseDto> CreateRestaurantAsync(CreateRestaurantRequest createRestaurantRequest)
    {
        var entity = Mapper.Map<CreateRestaurantRequest, Restaurant>(createRestaurantRequest);

        entity.OwnerId = _currentUser.UserId!.Value;

        var count = Queryable.Count();
        entity.Order = count + 1;

        await Repository.AddAsync(entity);
        await Repository.SaveAsync();
        Logger.LogInformation("Created new restaurant: {@Restaurant}", entity);
        return new ResponseDto { Id = entity.Id };
    }


    public async Task UpdateRestaurantAsync(int id, UpdateRestaurantRequest dto)
    {
        var restaurant = await Queryable
            .Include(r => r.Translations)
            .FirstAsync(r => r.Id == id);
        
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

    public async Task<RestaurantResponse> GetRestaurantByIdAsync(int id)
    {
        var result =
            await GetByIdProjectedAsync<RestaurantResponse>(id, includes: [r => r.Translations],
                trackingBehavior: TrackingBehavior.AsNoTracking);
        return result;
    }

    #region Get Restaurant Menu Async

    public async Task<RestaurantMenuDto> GetRestaurantMenuAsync(int restaurantId)
    {
        var query = Repository.GetQueryable()
                .Include(r => r.Translations)
                .Include(r => r.Categories)
                .ThenInclude(c => c.Translations)

                .Include(r => r.Categories)
                .ThenInclude(c => c.Sections)
                .ThenInclude(s => s.Translations)

                .Include(r => r.Categories)
                .ThenInclude(c => c.Sections)
                .ThenInclude(s => s.MenuItems)

                .Include(r => r.Categories)
                .ThenInclude(c => c.Sections)
                .ThenInclude(s => s.MenuItems)
                .ThenInclude(m => m.Translations)
                
                .Include(r => r.Categories)
                .ThenInclude(c => c.Sections)
                .ThenInclude(s => s.MenuItems)
                .ThenInclude(m => m.Variants)
                .ThenInclude(v => v.Translations)
            ;

        var result = await GetAllProjectedAsync<RestaurantMenuDto>(
            query: query,
            predicate: r => r.Id == restaurantId);
        return result.First();
    }

    #endregion

    public async Task<string> EditImageAsync(int id, ImageDto image)
    {
        var restaurant = await Queryable.FirstOrDefaultAsync(i => i.Id == id);

        if (restaurant == null)
            throw new ValidationException(Resources.NotFound);

        var imagePath = await _fileService.SaveFileAsync(image.File, "restaurant");

        restaurant.LogoPath = imagePath;
        await Repository.SaveAsync();

        return imagePath;
    }

    public async Task UpdateRestaurantOrderAsync(List<OrderDto> dto)
    {
        var allCategoriesCount = Queryable.Count();
        if (allCategoriesCount != dto.Count)
            throw new ValidationException(Resources.WrongNumberOfObjects);

        var orderMap = dto.ToDictionary(d => d.Id, d => d.Order);

        var restaurantIds = orderMap.Keys.ToList();
        var restaurants = await Queryable
            .Where(c => restaurantIds.Contains(c.Id))
            .ToListAsync();

        foreach (var restaurant in restaurants)
        {
            if (!orderMap.TryGetValue(restaurant.Id, out var newOrder)) continue;
            if (restaurant.Order != newOrder)
            {
                restaurant.Order = newOrder;
            }
        }

        await Repository.SaveAsync();
    }
    public async Task<IEnumerable<RestaurantDto>> RestaurantDetailList()
    {
        var result = await GetAllProjectedAsync<RestaurantDto>
        (predicate: r => r.ActivityPeriod.IsActive, includes: [r => r.Translations],
            trackingBehavior: TrackingBehavior.AsNoTracking);

        return result;
    }

    public async Task<RestaurantDetailDto> RestaurantDetail(int id)
    {
        var query = Repository.GetQueryable();
        var result = await GetByIdProjectedAsync<RestaurantDetailDto>
        (id, query: query, predicate: r => r.ActivityPeriod.IsActive, includes: [r => r.Translations],
            trackingBehavior: TrackingBehavior.AsNoTracking);

        return result;
    }
}