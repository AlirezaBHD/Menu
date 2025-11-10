using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Authentication;

public class LoginResponse
{
    [Required]
    public string Token { get; set; }
    
    [Required]
    public string Username { get; set; }
}