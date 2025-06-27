using OrderManagementSystem.DTOs;

namespace OrderManagementSystem.Services.Interfaces
{
    public interface IPaymentService
    {
        public Task ProcessPaymentAsync(int orderId);
        public Task<int> CreateOrderAsync(OrderDto dto);
    }
}
