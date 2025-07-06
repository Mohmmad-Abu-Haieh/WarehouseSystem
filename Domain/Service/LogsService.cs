using Domain.DTO.Logs;
using Domain.Interface;
using Domain.Repositories;
using DTO;
using Entity;
using Entity.Context;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service
{
    public class LogsService : ILogsService
    {
        private readonly AppDbContext _context;
        private readonly IRepository<User> _repository;

        public LogsService(AppDbContext context, IRepository<User> repository)
        {
            _context = context;
            _repository = repository;
        }

        public Task<DataTable<LogsData>> GetLogsDataTable(LogsFilter filter)
        {
            throw new NotImplementedException();
        }
        //public async Task<DataTable<LogsData>> GetLogsDataTable(LogsFilter filter)
        //{
        //    bool fiteredByKeyword = !string.IsNullOrEmpty(filter.Keyword);
        //    var query = _context.Users
        //                         .Where(item => item.Active
        //                                &&
        //                                 (fiteredByKeyword ?
        //                                   item.FullName.Contains(filter.Keyword)
        //                                || item.Email.Contains(filter.Keyword)
        //                                || item.Mobile.Contains(filter.Keyword)
        //                            : true)
        //                            )
        //                          .OrderByDescending(item => item.CreatedOn);

        //    int count = await query.CountAsync();
        //    var data = await query
        //                    .Skip(param.PageIndex * param.PageSize)
        //                    .Take(param.PageSize)
        //                    .Select(item => new LogsData
        //                    {
        //                        Id = item.Id,
        //                        Error = item.Error,

        //                    }).ToListAsync();
        //    var result = new DataTable<LogsData>
        //    {
        //        Data = data,
        //        Count = count
        //    };
        //    return result;
        //}
    }
}
