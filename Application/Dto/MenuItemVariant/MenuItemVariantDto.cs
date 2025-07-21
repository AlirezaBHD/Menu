namespace Application.Dto.MenuItemVariant;

public class MenuItemVariantDto
{
    public Guid Id { get; set; }
    
    public string Detail { get; set; }  = "";

    public decimal Price { get; set; }

    public bool IsAvailable { get; set; }
}