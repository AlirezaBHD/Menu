using Microsoft.AspNetCore.Http;

namespace Muno.Application.Dto.Shared;

public class ImageDto
{
    public IFormFile File { get; set; }
}