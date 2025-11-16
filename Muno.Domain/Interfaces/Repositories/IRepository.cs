namespace Muno.Domain.Interfaces.Repositories;

public enum TrackingBehavior
{
    Default,
    AsNoTracking,
    AsNoTrackingWithIdentityResolution
}

public interface IRepository<T> where T : class
{
    Task AddAsync(T entity);
    
    void Remove(T entity);
    
    void Update(T entity);
    
    Task SaveAsync();
    
    Task<T> GetByIdAsync(int id);
    
    IQueryable<T> GetQueryable();
    
    IQueryable<T> GetLimitedQueryable();
}