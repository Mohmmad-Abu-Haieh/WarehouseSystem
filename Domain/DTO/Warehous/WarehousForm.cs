using Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Warehous
{
    public class WarehousForm
    {
        public Guid? Id { get; set; }

        public string Code { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }
        
    }
}
