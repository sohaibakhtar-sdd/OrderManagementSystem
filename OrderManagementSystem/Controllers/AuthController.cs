using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.DTOs;
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
                return BadRequest("Weak Password");
            }
            if (await _IAuthService.IsEmailRegisteredAsync(dto.Email))
            {
                return Conflict("Email Already Exists");
            }

             var data =await _IAuthService.RegisterAsync(dto);
            return Ok(data);
        }
    }
}
