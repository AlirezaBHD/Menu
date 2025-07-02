using Application.Dto.Section;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SectionController : ControllerBase
{
    private readonly ISectionService _sectionService;

    public SectionController(ISectionService sectionService)
    {
        _sectionService = sectionService;
    }

    [HttpPost("/api/category/{categoryId}/[controller]")]
    public async Task<IActionResult> CreateSection([FromRoute] Guid categoryId,
        [FromBody] CreateSectionRequest createSectionRequest)
    {
        var section = await _sectionService.CreateSectionAsync(categoryId: categoryId,
            createSectionRequest: createSectionRequest);
        return Ok(section);
    }
}