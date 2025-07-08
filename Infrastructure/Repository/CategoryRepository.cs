using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infrastructure.Persistence;

namespace Infrastructure.Repository;

public class CategoryRepository:Repository<Category>, ICategoryRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ICurrentUser _currentUser;
    protected override IQueryable<Category> LimitedQuery =>
        base.LimitedQuery.Where(c => c.Restaurant!.OwnerId == _currentUser.UserId);

    public CategoryRepository(ApplicationDbContext context, ICurrentUser currentUser) : base(context)
    {
        _context = context;
        _currentUser = currentUser;
    }
}