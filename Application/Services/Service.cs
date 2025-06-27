using System.ComponentModel;
using System.Linq.Expressions;
using Application.Exceptions;
using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.RepositoryInterfaces;
using Infrastructure.QueryHelpers;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class Service<T> : IService<T> where T : class
{
    #region Injections

    private readonly IMapper _mapper;
    private readonly IRepository<T> _repository;
    private readonly IQueryable<T> _queryable;
    private readonly string _displayName;

    public Service(IMapper mapper, IRepository<T> repository)
    {
        _mapper = mapper;
        _repository = repository;
        _queryable = repository.GetQueryable();
        _displayName = typeof(T)
            .GetCustomAttributes(typeof(DisplayNameAttribute), true)
            .OfType<DisplayNameAttribute>()
            .FirstOrDefault()?.DisplayName ?? typeof(T).Name;
    }

    #endregion

    #region Get All Projected Async

    public async Task<IEnumerable<TDto>> GetAllProjectedAsync<TDto>(
        Expression<Func<T, bool>>? predicate = null,
        Expression<Func<T, object>>[]? includes = null,
        TrackingBehavior trackingBehavior = TrackingBehavior.Default,
        bool orderByNewest = true,
        IQueryable<T>? query = null)
    {
        query ??= _queryable;

        if (orderByNewest)
        {
            query = query.OrderByNewest();
        }

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        switch (trackingBehavior)
        {
            case TrackingBehavior.AsNoTracking:
                query = query.AsNoTracking();
                break;
            case TrackingBehavior.AsNoTrackingWithIdentityResolution:
                query = query.AsNoTrackingWithIdentityResolution();
                break;
            case TrackingBehavior.Default:
            default:
                break;
        }

        var result = await query.ProjectTo<TDto>(_mapper.ConfigurationProvider).ToListAsync();
        return result;
    }

    #endregion

    #region Get By Id Projecte dAsync

    public async Task<TDto> GetByIdProjectedAsync<TDto>(
        Guid id,
        Expression<Func<T, bool>>? predicate = null,
        Expression<Func<T, object>>[]? includes = null,
        TrackingBehavior trackingBehavior = TrackingBehavior.Default,
        IQueryable<T>? query = null)
    {
        query ??= _queryable;

        if (includes != null)
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        switch (trackingBehavior)
        {
            case TrackingBehavior.AsNoTracking:
                query = query.AsNoTracking();
                break;
            case TrackingBehavior.AsNoTrackingWithIdentityResolution:
                query = query.AsNoTrackingWithIdentityResolution();
                break;
            case TrackingBehavior.Default:
            default:
                break;
        }

        var obj = await query.ProjectTo<TDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(e => EF.Property<Guid>(e!, "Id") == id);
        
        if (obj == null)
            throw new NotFoundException(_displayName);
        
        return obj;
    }

    #endregion
    
    #region Get All Async

    public async Task<IEnumerable<T>> GetAllAsync(
        Expression<Func<T, bool>>? predicate = null,
        Expression<Func<T, object>>[]? includes = null,
        IQueryable<T>? query = null)
    {
        query ??= _queryable;

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        

        var result = await query.ToListAsync();
        return result;
    }

    #endregion
}