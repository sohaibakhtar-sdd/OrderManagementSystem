using OrderManagementSystem.Data;
using OrderManagementSystem.DTOs;
using OrderManagementSystem.Models;
using OrderManagementSystem.Services.Interfaces;

namespace OrderManagementSystem.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly OrderDbContext _context;
        public UserService(OrderDbContext context)
        {
            _context = context;
        }

        public Task<User> GetUserByIdAsync(int id)
        {
            var dataById = _context.Users.FirstOrDefault(u => u.Id == id);
            return Task.FromResult(dataById);

        }
        public Task<User> GetUserByEmailAsync(string email)
        {
            var dataByEmail = _context.Users.FirstOrDefault(u => u.Email == email);
            return Task.FromResult(dataByEmail);
        }
    }
}
