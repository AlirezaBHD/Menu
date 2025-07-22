using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

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

            query = query.Where(r => r.Id == _currentUser.RestaurantId);

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