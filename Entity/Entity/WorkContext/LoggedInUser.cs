using System;
using System.Collections.Generic;

namespace Entity.WorkContext
{
    public class LoggedInUser
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public Guid? RoleId { get; set; }
    }
}
