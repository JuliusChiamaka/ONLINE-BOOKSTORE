using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBookstore.Application.Interfaces.Service;
using OnlineBookstore.Application.Services;
using OnlineBookstore.Domain.Dtos.Request;

namespace OnlineBookstore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("getUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("getUser/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost("addUser")]
        public async Task<IActionResult> AddUser(AddUserRequest request)
        {
            await _userService.AddUserAsync(request);
            return Ok(); 
        }

        [HttpPut("updateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserRequest request)
        {
            await _userService.UpdateUserAsync(id, request);
            return NoContent();
        }

        [HttpDelete("deleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
    }

}
