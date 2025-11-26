using Microsoft.AspNetCore.Http;
using Muno.Application.Services.Interfaces;

namespace Muno.Application.Services;

public class FileService : IFileService
{
    private readonly string _uploadPath;
    private readonly string _basePath;

    public FileService()
    {
        _uploadPath = "uploads";
        _basePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", _uploadPath);
        if (!Directory.Exists(_basePath))
            Directory.CreateDirectory(_basePath);
    }

    public async Task<string> SaveFileAsync(IFormFile file, string directory)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("Invalid file");

        var categoryPath = Path.Combine(_basePath, directory);

        if (!Directory.Exists(categoryPath))
            Directory.CreateDirectory(categoryPath);

        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(categoryPath, fileName);

        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return Path.Combine(_uploadPath, directory, fileName);
    }

    public void DeleteFile(string relativePath)
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath);
        if (File.Exists(filePath))
            File.Delete(filePath);
    }
}