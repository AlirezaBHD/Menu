using Microsoft.AspNetCore.Http;

namespace Application.Services.Interfaces;

public interface IFileService
{
    Task<string> SaveFileAsync(IFormFile file, string directory);
}