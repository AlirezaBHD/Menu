using Domain.Common.Attributes;
using Muno.Application.Dto.Shared;

namespace Muno.Application.Dto.MenuItem;

public class MenuItemTranslationDto : TranslationDto
{
    [MultiLanguageProperty] 
    public string Title { get; set; }
    
    [MultiLanguageProperty] 
    public string? Description { get; set; }
}