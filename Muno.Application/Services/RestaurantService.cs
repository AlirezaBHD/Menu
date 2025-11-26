using System.Linq.Expressions;
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
using Muno.Domain.Entities;

namespace Muno.Application.Services;

public class RestaurantService(
    IRestaurantRepository restaurantRepository,
    IMapper mapper,
    IFileService fileService,
    ILogger<Restaurant> logger,
    ICurrentUser currentUser)
    : Service<Restaurant>(mapper, restaurantRepository, logger), IRestaurantService
{
    private static Expression<Func<Restaurant, bool>> IsAvailable(TimeSpan nowTime)
    {
        return r =>
            r.ActivityPeriod.IsActive && (
                r.ActivityPeriod.ActivityType == ActivityEnum.Unlimited ||
                (
                    (r.ActivityPeriod.ActivityType == ActivityEnum.ActivePeriod &&
                     nowTime >= r.ActivityPeriod.FromTime &&
                     nowTime <= r.ActivityPeriod.ToTime)
                    ||
                    (r.ActivityPeriod.ActivityType == ActivityEnum.InactivePeriod &&
                     (nowTime < r.ActivityPeriod.FromTime ||
                      nowTime > r.ActivityPeriod.ToTime))
                ));
    }
    public async Task<ResponseDto> CreateRestaurantAsync(CreateRestaurantRequest createRestaurantRequest)
    {
        var entity = Mapper.Map<CreateRestaurantRequest, Restaurant>(createRestaurantRequest);

        entity.OwnerId = currentUser.UserId!.Value;

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
            var imagePath = await fileService.SaveFileAsync(dto.LogoFile, "restaurant-logos");
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


    public async Task<string> EditImageAsync(int id, ImageDto image)
    {
        var restaurant = await Queryable.FirstOrDefaultAsync(i => i.Id == id);

        if (restaurant == null)
            throw new ValidationException(Resources.NotFound);

        var imagePath = await fileService.SaveFileAsync(image.File, "restaurant");

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
        var nowTime = DateTime.Now.TimeOfDay;
        var predicate = IsAvailable(nowTime);

        var result = await GetAllProjectedAsync<RestaurantDto>
        (predicate: predicate, includes: [r => r.Translations],
            trackingBehavior: TrackingBehavior.AsNoTracking);

        return result;
    }


    public async Task<RestaurantDetailDto> RestaurantDetail(int id)
    {
        var nowTime = DateTime.Now.TimeOfDay;
        var predicate = IsAvailable(nowTime);
        
        var query = Repository.GetQueryable();
        var result = await GetByIdProjectedAsync<RestaurantDetailDto>
        (id, query: query, predicate: predicate, includes: [r => r.Translations],
            trackingBehavior: TrackingBehavior.AsNoTracking);

        return result;
    }
}