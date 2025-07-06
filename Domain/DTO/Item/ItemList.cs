using Entity.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Item
{
    public class ItemList
    {
        public Guid? Id { get; set; }

        public string ItemName { get; set; }
        public string SkuCode { get; set; }
        public int Qty { get; set; }
        public decimal CostPrice { get; set; }
        public decimal MsrpPrice { get; set; }
        public string Warehouse { get; set; }
    }
}
