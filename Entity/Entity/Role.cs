using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersModule.Entity;

namespace Entity.Entity
{
    public class Role : BaseEntity<Guid>
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
