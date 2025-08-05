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

    public Guid? UserId => 
        TryParse(_httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier), out var id)
            ? id
            : null;

    public Guid RestaurantId => GetCurrentRestaurantId();
    private Guid GetCurrentRestaurantId()
    {
        var value = _httpContextAccessor.HttpContext?.Session.GetString("CurrentRestaurantId");
        if (TryParse(value, out var restaurantId))
            return restaurantId;
        return Empty;
    }
}
