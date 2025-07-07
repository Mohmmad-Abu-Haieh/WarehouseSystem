using Entity.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Warehous
{
    public class WarehousForm
    {
        public Guid? Id { get; set; }
        [Required(ErrorMessage = "This feild is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This feild is required")]

        public string Address { get; set; }
        [Required(ErrorMessage = "This feild is required")]

        public string City { get; set; }
        public Guid CountryId { get; set; }
    }
}
