namespace Domain.Interfaces.Services;

public interface ICurrentUser
{
    int? UserId { get; }
    int RestaurantId { get; }
    List<string> Roles { get; }
}
