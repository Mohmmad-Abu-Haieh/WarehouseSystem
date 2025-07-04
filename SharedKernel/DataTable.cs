using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedKernel
{
    [NotMapped]
    public class DataTable<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int Count { get; set; }
    }

    [NotMapped]
    public class DataTableMobile<T>
    {
        public T Data { get; set; }
        public int Count { get; set; }
    }
}
