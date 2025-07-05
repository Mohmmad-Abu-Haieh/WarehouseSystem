using Domain.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetWarehouseStatus()
        {
            return Ok(await _dashboardService.GetWarehouseStatusesAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetTopHighItems()
        {
            return Ok(await _dashboardService.GetTopItemsAsync(true));
        }

        [HttpGet]
        public async Task<IActionResult> GetTopLowItems()
        {
            return Ok(await _dashboardService.GetTopItemsAsync(false));
        }
    }
}
