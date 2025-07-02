namespace Application.Dto.Category;

public class CreateCategoryRequest
{
    public Guid RestaurantId { get; set; }
    public string? Title { get; set; }
}