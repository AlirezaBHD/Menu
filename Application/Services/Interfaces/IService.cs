using System.Linq.Expressions;
using Domain.RepositoryInterfaces;

namespace Application.Services.Interfaces;

public interface IService<T> where T : class
{
    Task<IEnumerable<TDto>> GetAllProjectedAsync<TDto>(
        Expression<Func<T, bool>>? predicate = null,
        Expression<Func<T, object>>[]? includes = null,
        TrackingBehavior trackingBehavior = TrackingBehavior.Default,
        bool orderByNewest = true,
        IQueryable<T>? query = null);

    Task<TDto> GetByIdProjectedAsync<TDto>(
        Guid id,
        Expression<Func<T, bool>>? predicate = null,
        Expression<Func<T, object>>[]? includes = null,
        TrackingBehavior trackingBehavior = TrackingBehavior.Default,
        IQueryable<T>? query = null);

    Task<IEnumerable<T>> GetAllAsync(
        Expression<Func<T, bool>>? predicate = null,
        Expression<Func<T, object>>[]? includes = null,
        IQueryable<T>? query = null);
}