using Domain.Entities.MenuItems;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class MenuItemRepository : Repository<MenuItem>, IMenuItemRepository
{
    #region Injection

    private readonly ApplicationDbContext _context;
    private readonly ICurrentUser _currentUser;

    protected override IQueryable<MenuItem> LimitedQuery
    {
        get
        {
            IQueryable<MenuItem> query = base.LimitedQuery;

            query = query
                .Include(m => m.Section)
                .ThenInclude(s => s!.Category)
                .ThenInclude(c => c!.Restaurant)
                .Where(m => m.Section!.Category!.RestaurantId == _currentUser.RestaurantId);

            return query;
        }
    }

    public MenuItemRepository(ApplicationDbContext context, ICurrentUser currentUser) : base(context)
    {
        _context = context;
        _currentUser = currentUser;
    }

    #endregion
}