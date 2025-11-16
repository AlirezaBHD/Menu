using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Muno.Application.Services;

public static class IdentitySeeder
{
    public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

        string[] roles = { "SuperAdmin", "Moderator" };

        foreach (var roleName in roles)
        {
            var exists = await roleManager.RoleExistsAsync(roleName);
            if (!exists)
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>
                {
                    Name = roleName,
                    NormalizedName = roleName.ToUpper()
                });
            }
        }
    }
}
