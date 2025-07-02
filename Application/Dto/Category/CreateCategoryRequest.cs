namespace Application.Dto.Category;

public class CreateCategoryRequest
{
    public int RestaurantId { get; set; }
    public string? Title { get; set; }
}