using Microsoft.AspNetCore.Http;

namespace Application.Dto.MenuItem;

public class MenuItemImageDto
{
    public IFormFile File { get; set; }
}