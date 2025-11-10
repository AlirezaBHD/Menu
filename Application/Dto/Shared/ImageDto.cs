using Microsoft.AspNetCore.Http;

namespace Application.Dto.Shared;

public class ImageDto
{
    public IFormFile File { get; set; }
}