using Microsoft.AspNetCore.Mvc;
using Tienda.Application.Dtos.Request;
using Tienda.Application.Interfaces;
using Tienda.Infrastructure.Commons.Bases.Request;

namespace Tienda.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserApplication _userApplication;
        public UserController(IUserApplication userApplication)
        {
            _userApplication = userApplication;
        }

        [HttpPost]
        public async Task<IActionResult> ListUsers([FromBody] BaseFiltersRequest filters)
        {
            var response = await _userApplication.ListUsers(filters);
            return Ok(response);
        }

        [HttpGet("Select")]
        public async Task<IActionResult> ListSelectUsers()
        {
            var response = await _userApplication.ListSelectUsers();
            return Ok(response);
        }

        [HttpGet("{userId:int}")]
        public async Task<IActionResult> UserById(int userId)
        {
            var response = await _userApplication.UserById(userId);
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(UserRequestDto requestDto)
        {
            var response = await _userApplication.RegisterUser(requestDto);
            return Ok(response);
        }

        [HttpPut("Edit/{userId:int}")]
        public async Task<IActionResult> EditUser(int userId, UserRequestDto requestDto)
        {
            var response = await _userApplication.EditUser(userId, requestDto);
            return Ok(response);
        }

        [HttpPut("Remove/{userId:int}")]
        public async Task<IActionResult> RemoveUser(int userId)
        {
            var response = await _userApplication.RemoveUser(userId);
            return Ok(response);
        }
    }
}
