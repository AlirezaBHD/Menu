using Muno.Domain.Entities.Restaurants;
using Muno.Domain.Interfaces.Repositories;
using Muno.Domain.Interfaces.Services;
using Infrastructure.Persistence;

namespace Infrastructure.Repository;

public class RestaurantRepository : Repository<Restaurant>, IRestaurantRepository
{
    #region Injection

    private readonly ApplicationDbContext _context;
    private readonly ICurrentUser _currentUser;

    protected override IQueryable<Restaurant> LimitedQuery
    {
        get
        {
            IQueryable<Restaurant> query = base.LimitedQuery;
            
            var roles = _currentUser.Roles;
            
            if (roles.Any(r => r is "SuperAdmin" or "Moderator"))
            {
                query = query.Where(r => r.OwnerId == _currentUser.UserId);
            }
            else
            {
                query = query.Where(r => r.Id == _currentUser.RestaurantId);
            }

            return query;
        }
    }

    public RestaurantRepository(ApplicationDbContext context, ICurrentUser currentUser) : base(context)
    {
        _context = context;
        _currentUser = currentUser;
    }

    #endregion
}