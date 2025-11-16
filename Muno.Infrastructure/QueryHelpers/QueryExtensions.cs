using Microsoft.EntityFrameworkCore;

namespace Infrastructure.QueryHelpers;

public static class QueryExtensions
{
    public static IQueryable<T> OrderByNewest<T>(this IQueryable<T> query) where T : class
    {
        return query.OrderByDescending(x => EF.Property<object>(x, "CreatedOn"));
    }
}