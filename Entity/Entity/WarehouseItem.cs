using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersModule.Entity;

namespace Entity.Entity
{
    public class WarehouseItem : BaseEntity<Guid>
    {
        [Required]
        [MaxLength(100)]
        public string ItemName { get; set; }
        public string SkuCode { get; set; }

        [Range(1, int.MaxValue)]
        public int Qty { get; set; }
        [Required]
        public decimal CostPrice { get; set; }
        public decimal MsrpPrice { get; set; }
        public Guid WarehouseId { get; set; }
        [ForeignKey("WarehouseId")]
        public Warehouse Warehouse { get; set; }
    }
}
