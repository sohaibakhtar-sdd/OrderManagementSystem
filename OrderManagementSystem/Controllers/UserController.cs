using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.DTOs;
using OrderManagementSystem.Models;
using OrderManagementSystem.Services.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OrderManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _IUserService;
        public UserController(IUserService IUserService)
        {
            _IUserService = IUserService;
        }

        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetUserById(int Id) 
        {
            var userById = await  _IUserService.GetUserByIdAsync(Id);
            if (userById == null)
            {
                return BadRequest("Not Found");
            }
            else
            {
                return Ok(new
                {
                    message = "User fetched successfully",
                    data = userById
                });
            }
        }
    }
}
