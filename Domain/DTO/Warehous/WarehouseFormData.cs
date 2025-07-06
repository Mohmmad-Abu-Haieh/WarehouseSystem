using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Warehous
{
    public class WarehouseFormData
    {
        public IEnumerable<Hook<Guid, string>> Countries { get; set; }

    }
}
