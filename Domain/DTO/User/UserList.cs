﻿using System;

namespace DTO
{
  public class UserList
    {
        public Guid? Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
    }
}
