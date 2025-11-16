using Muno.Domain.Entities;
using Muno.Domain.Entities.Categories;
using Muno.Domain.Interfaces.Repositories;
using Muno.Domain.Interfaces.Services;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    #region Injection

    private readonly ApplicationDbContext _context;
    private readonly ICurrentUser _currentUser;

    protected override IQueryable<Category> LimitedQuery
    {
        get
        {
            IQueryable<Category> query = base.LimitedQuery;

            query = query
                .Include(c => c!.Restaurant)
                .Where(c => c.RestaurantId == _currentUser.RestaurantId);

            return query;
        }
    }

    public CategoryRepository(ApplicationDbContext context, ICurrentUser currentUser) : base(context)
    {
        _context = context;
        _currentUser = currentUser;
    }

    #endregion
}