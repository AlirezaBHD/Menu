using Muno.Application.Dto.Authentication;

namespace Muno.Application.Services.Interfaces;

public interface IAuthService
{
    Task CreateAdminAsync(RegisterAdminRequest request);
    
    Task<LoginResponse> LoginAsync(LoginRequest request);
}