using SharedKernel;
using System;
using System.Collections.Generic;

namespace DTO
{
    public class UserFormData
    {
        public IEnumerable<Hook<Guid,string>> Roles { get; set; }
    }
}
