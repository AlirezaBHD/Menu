using Application.Dto.Authentication;

namespace Application.Services.Interfaces;

public interface IAuthService
{
    Task CreateAdminAsync(RegisterAdminRequest request);
    Task<LoginResponse> LoginAsync(LoginRequest request);
}