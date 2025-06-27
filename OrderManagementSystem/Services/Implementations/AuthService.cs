using OrderManagementSystem.Data;
using OrderManagementSystem.DTOs;
using OrderManagementSystem.Models;
using OrderManagementSystem.Services.Interfaces;

namespace OrderManagementSystem.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _IUserService;
        private readonly OrderDbContext _context;
        public AuthService(IUserService IUserService, OrderDbContext context)
        {
            _IUserService = IUserService;
            _context = context;
        }
        public async Task<bool> IsEmailRegisteredAsync(string email)
        {
            var data = await _IUserService.GetUserByEmailAsync(email);
            if(data!= null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            else
            {
                return password.Length >= 8 &&
                   password.Any(char.IsUpper) &&
                   password.Any(char.IsLower) &&
                   password.Any(char.IsDigit);
            }
        }
        public async Task<CommonResponse> RegisterAsync(UserDto user)
        {
            CommonResponse response = new ();
            var userdata = new User();
            {
                userdata.Email = user.Email;
                userdata.Password = user.Password;
            }
            _context.Users.Add(userdata);
            await _context.SaveChangesAsync();
            response.StatusCode = "200";
            response.Message = "Successfully Registered.";
            return response;
        }
    }
}
