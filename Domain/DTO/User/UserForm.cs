using Entity;
using System;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
   public class UserForm
    {
        public Guid? Id { get; set; }
        [Required(ErrorMessage = "Full Name is required.")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
        public string Mobile { get; set; }
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]

        public string Password { get; set; }
        [Required(ErrorMessage = "Role is required.")]

        public Guid? RoleId { get; set; }
    }
}
