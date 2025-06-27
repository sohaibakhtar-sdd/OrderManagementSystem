using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.DTOs;
using OrderManagementSystem.Models;
using OrderManagementSystem.Services.Implementations;
using OrderManagementSystem.Services.Interfaces;

namespace OrderManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly  IAuthService _IAuthService;
        public AuthController(IAuthService IAuthService)
        {
            _IAuthService = IAuthService;   
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto dto)
        {
            if (!_IAuthService.IsValidPassword(dto.Password))
            { 
                return BadRequest("Weak password");
            }
            if (await _IAuthService.IsEmailRegisteredAsync(dto.Email))
            {
                return Conflict("Email already exists");
            }

             var data =await _IAuthService.RegisterAsync(dto);
            return Ok(data);
        }
    }
}
