using Muno.Domain.Entities.Categories;
using Muno.Domain.Interfaces.Repositories;
using Muno.Domain.Interfaces.Services;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class CategoryRepository(ApplicationDbContext context, ICurrentUser currentUser)
    : Repository<Category>(context), ICategoryRepository
{
    protected override IQueryable<Category> LimitedQuery
    {
        get
        {
            var query = base.LimitedQuery;

            query = query
                .Include(c => c.Restaurant)
                .Where(c => c.RestaurantId == currentUser.RestaurantId);

            return query;
        }
    }
}