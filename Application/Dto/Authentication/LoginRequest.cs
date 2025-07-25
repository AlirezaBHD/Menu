using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Authentication;

public class LoginRequest
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}