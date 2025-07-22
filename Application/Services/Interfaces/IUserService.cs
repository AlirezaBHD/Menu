using Application.Dto.User;
using Domain.Entities;

namespace Application.Services.Interfaces;

public interface IUserService: IService<ApplicationUser>
{
    Task<IEnumerable<UserRestaurantsDto>> Restaurants();
    Task SetRestaurantIdInSessionAsync(Guid restaurantId);
}