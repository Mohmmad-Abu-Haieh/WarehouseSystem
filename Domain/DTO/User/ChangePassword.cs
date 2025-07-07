using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class ChangePassword
    {
        public Guid Id { get; set; }

        public string NewPassword { get; set; }
    }
}
