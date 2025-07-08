using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Logs
{
    public class LogsFilter
    {
        public string Keyword { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}
