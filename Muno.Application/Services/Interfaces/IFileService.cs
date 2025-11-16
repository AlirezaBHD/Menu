using Microsoft.AspNetCore.Http;

namespace Muno.Application.Services.Interfaces;

public interface IFileService
{
    Task<string> SaveFileAsync(IFormFile file, string directory);
}