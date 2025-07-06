using System.ComponentModel.DataAnnotations;
using UsersModule.Entity;

namespace Entity.Entity
{
    public class Role : BaseEntity<Guid>
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public RoleEnum? Code { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
    }

    public enum RoleEnum
    {
        Admin = 1,
        Management = 2,
        Auditor = 3
    }
}
