using Muno.Domain.Entities.Restaurants;
using Muno.Domain.Interfaces.Repositories;
using Muno.Domain.Interfaces.Services;
using Infrastructure.Persistence;

namespace Infrastructure.Repository;

public class RestaurantRepository(ApplicationDbContext context, ICurrentUser currentUser)
    : Repository<Restaurant>(context), IRestaurantRepository
{
    protected override IQueryable<Restaurant> LimitedQuery
    {
        get
        {
            var query = base.LimitedQuery;
            
            var roles = currentUser.Roles;
            
            if (roles.Any(r => r is "SuperAdmin" or "Moderator"))
            {
                query = query.Where(r => r.OwnerId == currentUser.UserId);
            }
            else
            {
                query = query.Where(r => r.Id == currentUser.RestaurantId);
            }

            return query;
        }
    }
}