namespace Domain.RepositoryInterfaces;

public enum TrackingBehavior
{
    Default,
    AsNoTracking,
    AsNoTrackingWithIdentityResolution
}

public interface IRepository<T> where T : class
{
    #region Add Async

    Task AddAsync(T entity);

    #endregion

    #region Remove

    void Remove(T entity);

    #endregion

    #region Update

    void Update(T entity);

    #endregion

    #region Save Async

    Task SaveAsync();

    #endregion

    #region Get By Id Async

    Task<T> GetByIdAsync(Guid id);

    #endregion

    #region Get IQuaryable

    Task<IQueryable<T>> GetQueryable();

    #endregion
}