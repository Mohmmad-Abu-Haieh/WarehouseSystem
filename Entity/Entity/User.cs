using Entity.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UsersModule.Entity;

namespace Entity
{
    public class User : BaseEntity<Guid>
    {
        [Required]
        [MaxLength(250)]
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        [Required]
        [MaxLength(250)]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(250)]
        public string Email { get; set; }
        [MaxLength(250)]
        public string Mobile { get; set; }
        public Guid? RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; }
    }
}

