using System.Security.Claims;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using static System.Guid;

namespace Application.Services;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int? UserId => 
        int.TryParse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier), out var id)
            ? id
            : null;

    public int RestaurantId => GetCurrentRestaurantId();
    public List<string> Roles => GetCurrentUserRoles();

    private List<string> GetCurrentUserRoles()
    {
        var user = _httpContextAccessor.HttpContext?.User;
        var roles = user?.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();

        return roles;
    }

    private int GetCurrentRestaurantId()
    {
        var value = _httpContextAccessor.HttpContext?.Session.GetString("CurrentRestaurantId");
        if (int.TryParse(value, out var restaurantId))
            return restaurantId;
        return 0;
    }
}
