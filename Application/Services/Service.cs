using System.ComponentModel;
using System.Linq.Expressions;
using Application.Exceptions;
using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces.Repositories;
using Infrastructure.QueryHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class Service<T> : IService<T> where T : class
{
    #region Injections

    protected readonly IMapper Mapper;
    protected readonly IRepository<T> Repository;
    protected readonly IQueryable<T> Queryable;
    protected readonly ILogger<T> Logger;
    private readonly string _displayName;

    public Service(IMapper mapper, IRepository<T> repository, ILogger<T> logger)
    {
        Mapper = mapper;
        Repository = repository;
        Logger = logger;
        Queryable = repository.GetLimitedQueryable();
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
        query ??= Queryable;

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

        var result = await query.ProjectTo<TDto>(Mapper.ConfigurationProvider).ToListAsync();
        return result;
    }

    #endregion

    #region Get By Id Projecte dAsync

    public async Task<TDto> GetByIdProjectedAsync<TDto>(
        int id,
        Expression<Func<T, bool>>? predicate = null,
        Expression<Func<T, object>>[]? includes = null,
        TrackingBehavior trackingBehavior = TrackingBehavior.Default,
        IQueryable<T>? query = null)
    {
        query ??= Queryable;

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

        var obj = await query.ProjectTo<TDto>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(e => EF.Property<int>(e!, "Id") == id);
        
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
        query ??= Queryable;

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