using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Item
{
    public class ItemFormData
    {
        public IEnumerable<Hook<Guid, string>> Warehouses { get; set; }

    }
}
