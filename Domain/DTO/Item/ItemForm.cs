using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Item
{
    public class ItemForm
    {
        public Guid? Id { get; set; }
        [Required(ErrorMessage = "Item name is required.")]
        [MaxLength(100, ErrorMessage = "Item name must not exceed 100 characters.")]

        public string ItemName { get; set; }
        public string SkuCode { get; set; }
        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]

        public int Qty { get; set; }
        [Required(ErrorMessage = "Cost price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Cost price must be greater than 0.")]

        public decimal CostPrice { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "MSRP price cannot be negative.")]

        public decimal MsrpPrice { get; set; }
        public Guid WarehouseId { get; set; }
    }
}
