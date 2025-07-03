using Application.Dto.Restaurant;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.RepositoryInterfaces;

namespace Application.Services;


public class RestaurantService :Service<Restaurant>, IRestaurantService
{
    #region Injection
    
    private readonly IFileService _fileService;

    public RestaurantService(IRestaurantRepository restaurantRepository, IMapper mapper, IFileService fileService) 
        : base(mapper, restaurantRepository)
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
            var imagePath = await _fileService.SaveFileAsync(dto.LogoFile, "Restaurant");
            restaurant.LogoPath = imagePath;
        }

        Repository.Update(restaurant);
        await Repository.SaveAsync();    
    }
}
