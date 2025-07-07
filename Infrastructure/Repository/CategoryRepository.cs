using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class CategoryRepository:Repository<Category>, ICategoryRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ICurrentUser _currentUser;

    public CategoryRepository(ApplicationDbContext context, ICurrentUser currentUser) : base(context)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public IQueryable<Category> OwnedCategoriesQuery()
    {
        var defaultQuery = _context.Categories.Include(c => c.Restaurant)
            .Where(c => c.Restaurant!.OwnerId == _currentUser.UserId);
        return defaultQuery;
    }
}