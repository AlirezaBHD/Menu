namespace Application.Dto.Authentication;

public class RegisterAdminRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}