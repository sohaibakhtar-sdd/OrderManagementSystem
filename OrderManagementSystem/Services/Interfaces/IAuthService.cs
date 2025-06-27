using OrderManagementSystem.DTOs;

namespace OrderManagementSystem.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<bool> IsEmailRegisteredAsync(string email);
        public bool IsValidPassword(string password);
        public Task<CommonResponse> RegisterAsync(UserDto user);
    }
}
