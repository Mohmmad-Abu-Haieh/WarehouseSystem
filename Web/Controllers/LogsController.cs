using Domain.DTO.Logs;
using Domain.Interface;
using Entity.WorkContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class LogsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogsService _logsService;
        private readonly SessionProvider _sessionProvider;

        public LogsController(IConfiguration configuration, ILogsService logsService, SessionProvider sessionProvider)
        {
            _configuration = configuration;
            _logsService = logsService;
            _sessionProvider = sessionProvider;
        }
        [HttpPost]
        public async Task<IActionResult> GetLogsDataTable([FromBody] LogsFilter filter)
        {
            var result = await _logsService.GetLogsDataTable(filter);
            return Ok(result);
        }
    }
}
