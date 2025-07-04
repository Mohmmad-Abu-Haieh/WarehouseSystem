using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersModule.Entity;

namespace Entity.Entity
{
    public class Warehouse : BaseEntity<Guid>
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [MaxLength(100)]
        public string City { get; set; }
        public Guid CountryId { get; set; }
        [ForeignKey("CountryId")]
        public Country Country { get; set; }
        public ICollection<WarehouseItem> Items { get; set; } = new List<WarehouseItem>();
    }
}
