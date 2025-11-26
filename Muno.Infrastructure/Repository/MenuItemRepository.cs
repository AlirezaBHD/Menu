using Muno.Domain.Entities.MenuItems;
using Muno.Domain.Interfaces.Repositories;
using Muno.Domain.Interfaces.Services;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class MenuItemRepository(ApplicationDbContext context, ICurrentUser currentUser)
    : Repository<MenuItem>(context), IMenuItemRepository
{
    protected override IQueryable<MenuItem> LimitedQuery
    {
        get
        {
            var query = base.LimitedQuery;

            query = query
                .Include(m => m.Section)
                .ThenInclude(s => s!.Category)
                .ThenInclude(c => c!.Restaurant)
                .Where(m => m.Section!.Category!.RestaurantId == currentUser.RestaurantId);

            return query;
        }
    }
}