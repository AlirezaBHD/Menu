using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence;

namespace Infrastructure.Repository;

public class MenuItemRepository:Repository<MenuItem>, IMenuItemRepository
{
    private readonly ApplicationDbContext _context;

    public MenuItemRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}