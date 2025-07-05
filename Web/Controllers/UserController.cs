using Domain.Interface;
using DTO;
using Entity.WorkContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly SessionProvider _sessionProvider;

        public UserController(IConfiguration configuration, IUserService userService, SessionProvider sessionProvider)
        {
            _configuration = configuration;
            _userService = userService;
            _sessionProvider = sessionProvider;
        }
        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] UserForm form)
        {
            var result = await _userService.CreateAccount(form, _sessionProvider.CurrentUser.FullName);
            return Ok(result);
        }
        // PUT: api/User/update
        [HttpPut("update")]
        public async Task<IActionResult> UpdateAccount([FromBody] UserForm form)
        {
            var result = await _userService.UpdateAccount(form);
            return Ok(result);
        }
        // GET: api/User/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userService.GetUserById(id);
            return user == null ? NotFound() : Ok(user);
        }
        // GET: api/User/username/{username}
        [HttpGet("username/{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var user = await _userService.GetUserByUsername(username);
            return user == null ? NotFound() : Ok(user);
        }
        // GET: api/User/username/{username}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserDetails(Guid id)
        {
            var user = await _userService.GetUserDetails(id);
            return user == null ? NotFound() : Ok(user);
        }
        // POST: api/User/filter
        [HttpPost]
        public async Task<IActionResult> GetUsersDataTable([FromBody] UserFilter filter)
        {
            var result = await _userService.GetUsersDataTable(filter);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                return Ok(await _userService.DeleteUser(id));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersFormData()
        {
            var data = await _userService.GetUsersFormData();
            return Ok(data);
        }
    }
}
