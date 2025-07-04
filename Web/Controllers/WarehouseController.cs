using Domain.DTO.Warehous;
using Domain.Interface;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Route("api/[controller]/[action]")]
    public class WarehouseController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IWarehouseService _warehouseService;

        public WarehouseController(IConfiguration configuration, IWarehouseService warehouseService)
        {
            _configuration = configuration;
            _warehouseService = warehouseService;
        }
        // POST: api/User
        [HttpPost("create")]
        public async Task<IActionResult> CreateWarehous([FromBody] WarehousForm form)
        {
            var result = await _warehouseService.CreateWarehous(form);
            return Ok(result);
        }
        // PUT: api/User/update
        [HttpPut("update")]
        public async Task<IActionResult> UpdateWarehouse([FromBody] WarehousForm form)
        {
            var result = await _warehouseService.UpdateWarehouse(form);
            return Ok(result);
        }
        // GET: api/User/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWarehouseById(Guid id)
        {
            var user = await _warehouseService.GetWarehouseById(id);
            return user == null ? NotFound() : Ok(user);
        }
        // GET: api/User/username/{username}
        [HttpGet("username/{username}")]
        public async Task<IActionResult> GetWarehouseByCode(string code)
        {
            var user = await _warehouseService.GetWarehouseByCode(code);
            return user == null ? NotFound() : Ok(user);
        }
        // POST: api/User/filter
        [HttpPost("filter")]
        public async Task<IActionResult> GetWarehouseDataTable([FromBody] WarehouseFilter filter)
        {
            var result = await _warehouseService.GetWarehouseDataTable(filter);
            return Ok(result);
        }
    }
}
