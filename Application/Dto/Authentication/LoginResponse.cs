using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Authentication;

public class LoginResponse
{
    [Required]
    public string Token { get; set; }
    public DateTime Expires { get; set; }
}