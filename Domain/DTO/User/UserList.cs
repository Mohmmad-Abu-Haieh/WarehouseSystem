using System;

namespace DTO
{
  public class UserList
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public Guid? RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
