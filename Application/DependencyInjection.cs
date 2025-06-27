using Application.Services;
using Application.Services.Interfaces;
using Domain.RepositoryInterfaces;
using Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IService<>), typeof(Service<>));
        services.AddScoped<IMenuItemService, MenuItemService>();
        services.AddScoped<ISectionService, SectionService>();
        services.AddScoped<ICategoryService, CategoryService>();

        return services;
    }

    public static IServiceCollection AddApplicationRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IMenuItemRepository, MenuItemRepository>();
        services.AddScoped<ISectionRepository, SectionRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        
        return services;

    }
}
