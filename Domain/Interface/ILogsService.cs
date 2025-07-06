using Domain.DTO.Logs;
using DTO;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface ILogsService
    {
        Task<DataTable<LogsData>> GetLogsDataTable(LogsFilter filter);

    }
}
