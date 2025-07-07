using Domain.DTO.Item;
using Domain.DTO.Warehous;
using Domain.Interface;
using Domain.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class ItemsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IItemService _itemService;
        private readonly ILogger<ItemsController> _logger;

        public ItemsController(IConfiguration configuration, IItemService itemService, ILogger<ItemsController> logger)
        {
            _configuration = configuration;
            _itemService = itemService;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> GetItemsDataTable([FromBody] ItemFilter filter)
        {
            var result = await _itemService.GetItemsDataTable(filter);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateItems([FromBody] ItemForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.ValidationState);

            var result = await _itemService.CreateItems(form);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateItem([FromBody] ItemForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.ValidationState);
            var result = await _itemService.UpdateItem(form);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetItemsFormData()
        {
            var data = await _itemService.GetItemsFormData();
            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemDetails(Guid id)
        {
            var user = await _itemService.GetItemDetails(id);
            return user == null ? NotFound() : Ok(user);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            try
            {
                return Ok(await _itemService.DeleteItem(id));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

