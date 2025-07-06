namespace Application.Dto.Authentication;

public class LoginResponse
{
    public string Token { get; set; }
    public DateTime Expires { get; set; }
}