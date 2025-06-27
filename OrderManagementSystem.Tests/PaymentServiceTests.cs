using Microsoft.EntityFrameworkCore;
using Moq;
using OrderManagementSystem.Data;
using OrderManagementSystem.DTOs;
using OrderManagementSystem.Models;
using OrderManagementSystem.Services.Implementations;
using OrderManagementSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Tests
{
    public class PaymentServiceTests
    {
        private readonly Mock<IPaymentService> _mockPaymentService;

        public PaymentServiceTests()
        {
            _mockPaymentService = new Mock<IPaymentService>();
        }
        private OrderDbContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<OrderDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new OrderDbContext(options);
        }

        // For Creating new Order
        [Fact]
        public async Task CreateOrderAsync_AddsOrderAndReturnsId()
        {
            //Arrange
            var context = GetInMemoryContext();
            var service = new PaymentService(context);
            var orderDto = new OrderDto
            {
                UserId = 1,
                Amount = 250.75m
            };

            var orderId = await service.CreateOrderAsync(orderDto);

            var savedOrder = await context.Orders.FindAsync(orderId);

            //Assert
            Assert.NotEqual(0, orderId);                     
            Assert.NotNull(savedOrder);                    
            Assert.Equal(orderDto.UserId, savedOrder.UserId);
            Assert.Equal(orderDto.Amount, savedOrder.Amount);
            Assert.False(savedOrder.IsPaid);
            Assert.Equal(orderId, savedOrder.Id);
        }

        // Valid Mark Order As Paid
        [Fact]
        public async Task ProcessPaymentAsync_MarksOrderAsPaid_WhenOrderExistsAndNotPaid()
        {

            var context = GetInMemoryContext();
            var order = new Order { UserId = 1, Amount = 100m, IsPaid = false };
            context.Orders.Add(order);
            await context.SaveChangesAsync();

            var service = new PaymentService(context);

            await service.ProcessPaymentAsync(order.Id);

            var updated = await context.Orders.FindAsync(order.Id);
            Assert.True(updated.IsPaid);
        }

        // Throw Invalid Exception When Order Not Found
        [Fact]
        public async Task ProcessPaymentAsync_Throws_WhenOrderNotFound()
        {
            // Arrange
            var context = GetInMemoryContext();
            var service = new PaymentService(context);

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(
                () => service.ProcessPaymentAsync(999)
            );
            Assert.Equal("Order not found", ex.Message);
        }

        // Throws Invalid_Exception When Already Paid
        [Fact]
        public async Task ProcessPaymentAsync_Throws_WhenOrderAlreadyPaid()
        {
            var context = GetInMemoryContext();
            var order = new Order { UserId = 2, Amount = 50m, IsPaid = true };
            context.Orders.Add(order);
            await context.SaveChangesAsync();

            var service = new PaymentService(context);

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(
                () => service.ProcessPaymentAsync(order.Id)
            );
            Assert.Equal("Order already paid", ex.Message);
        }
    }
}
