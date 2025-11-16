using Muno.Domain.Interfaces.Services;

namespace Muno.API.Middlewares;

public class UserIdEnricherMiddleware
{
    private readonly RequestDelegate _next;

    public UserIdEnricherMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, ICurrentUser user)
    {
        var userId = user.UserId.ToString();

        if (!string.IsNullOrEmpty(userId))
        {
            Serilog.Context.LogContext.PushProperty("UserId", userId);
        }

        await _next(context);
    }
}
