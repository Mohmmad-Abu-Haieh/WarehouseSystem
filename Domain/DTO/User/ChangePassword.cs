using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class ChangePassword
    {
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
