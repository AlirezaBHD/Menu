using Application.Dto.Restaurant;
using Domain.Entities;

namespace Application.Services.Interfaces;

public interface IUserService: IService<ApplicationUser>
{
    Task<IEnumerable<UserRestaurantsDto>> Restaurants();
}