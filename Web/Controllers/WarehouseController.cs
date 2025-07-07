using Domain.DTO.Warehous;
using Domain.Interface;
using Domain.Service;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class WarehouseController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IWarehouseService _warehouseService;
        private readonly ILogger<WarehouseController> _logger;


        public WarehouseController(IConfiguration configuration, IWarehouseService warehouseService, ILogger<WarehouseController> logger)
        {
            _configuration = configuration;
            _warehouseService = warehouseService;
            _logger = logger;

        }
        [HttpPost]
        public async Task<IActionResult> CreateWarehous([FromBody] WarehousForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.ValidationState);

            var result = await _warehouseService.CreateWarehous(form);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWarehouseDetails(Guid id)
        {
            var user = await _warehouseService.GetWarehouseDetails(id);
            return user == null ? NotFound() : Ok(user);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateWarehouse([FromBody] WarehousForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.ValidationState);
            var result = await _warehouseService.UpdateWarehouse(form);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWarehouseById(Guid id)
        {
            var user = await _warehouseService.GetWarehouseById(id);
            return user == null ? NotFound() : Ok(user);
        }
        [HttpGet("{username}")]
        public async Task<IActionResult> GetWarehouseByCode(string code)
        {
            var user = await _warehouseService.GetWarehouseByCode(code);
            return user == null ? NotFound() : Ok(user);
        }
        [HttpPost]
        public async Task<IActionResult> GetWarehouseDataTable([FromBody] WarehouseFilter filter)
        {
            var result = await _warehouseService.GetWarehouseDataTable(filter);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> GetWarehouseItemsDataTable([FromBody] WarehouseFilter filter)
        {
            var result = await _warehouseService.GetWarehouseItemsDataTable(filter);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetWarehouseFormData()
        {
            var data = await _warehouseService.GetWarehouseFormData();
            return Ok(data);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWarehouse(Guid id)
        {
            try
            {
                return Ok(await _warehouseService.DeleteWarehouse(id));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
