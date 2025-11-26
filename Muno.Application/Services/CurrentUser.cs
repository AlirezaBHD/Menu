using System.Security.Claims;
using Muno.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace Muno.Application.Services;

public class CurrentUser(IHttpContextAccessor httpContextAccessor) : ICurrentUser
{
    public int? UserId => 
        int.TryParse(httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier),
            out var id) ? id : null;

    
    public int RestaurantId => GetCurrentRestaurantId();
    
    
    public List<string> Roles => GetCurrentUserRoles();

    
    private List<string> GetCurrentUserRoles()
    {
        var user = httpContextAccessor.HttpContext?.User;
        
        if(user == null) return [];
        
        var roles = user.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();
        return roles;
    }

    
    private int GetCurrentRestaurantId()
    {
        var value = httpContextAccessor.HttpContext?.Session.GetString("CurrentRestaurantId");
        return int.TryParse(value, out var restaurantId) ? restaurantId : 0;
    }
}
