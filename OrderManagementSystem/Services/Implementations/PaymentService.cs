using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data;
using OrderManagementSystem.DTOs;
using OrderManagementSystem.Models;
using OrderManagementSystem.Services.Interfaces;

namespace OrderManagementSystem.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly OrderDbContext _context;
        public PaymentService(OrderDbContext context)
        {
            _context = context;
        }

        public async Task ProcessPaymentAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                throw new InvalidOperationException("Order not found");
            }
            if (order.IsPaid)
            {
                throw new InvalidOperationException("Order already paid");
            }

            order.IsPaid = true;
            await _context.SaveChangesAsync();
        }

        public async Task<int> CreateOrderAsync(OrderDto dto)
        {
            var order = new Order 
            { 
                UserId = dto.UserId,
                Amount = dto.Amount 
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order.Id;
        }
    }
}
