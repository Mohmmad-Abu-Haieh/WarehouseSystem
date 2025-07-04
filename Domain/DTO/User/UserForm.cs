using Entity;
using System;

namespace DTO
{
   public class UserForm
    {
        public Guid? Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public Guid? RoleId { get; set; }
    }
}
