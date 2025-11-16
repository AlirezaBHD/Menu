using Muno.Domain.Interfaces.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _entities;
    protected virtual IQueryable<T> LimitedQuery => _entities.AsQueryable();

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _entities = context.Set<T>();
    }
    
    public async Task AddAsync(T entity) => await _entities.AddAsync(entity);
    
    public void Remove(T entity) => _entities.Remove(entity);
    
    public void Update(T entity) => _entities.Update(entity);

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
    
    public async Task<T> GetByIdAsync(int id)
    {
        var obj = await LimitedQuery.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        return obj!;
    }
    
    public IQueryable<T> GetQueryable()
    {
        var query = _context.Set<T>().AsQueryable();
        return query;
    }
    
    public IQueryable<T> GetLimitedQueryable()
    {
        return LimitedQuery;
    }
}