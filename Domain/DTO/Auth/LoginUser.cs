using System.ComponentModel.DataAnnotations;

namespace DTO;

public class LoginUser
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}
