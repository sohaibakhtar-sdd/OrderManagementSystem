using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.DTOs;
using OrderManagementSystem.Services.Interfaces;

namespace OrderManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _IPaymentService;
        public PaymentController(IPaymentService IPaymentService)
        {
            _IPaymentService = IPaymentService;            
        }

        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder(OrderDto dto)
        {
            var id = await _IPaymentService.CreateOrderAsync(dto);
            return Ok(new { OrderId = id });
        }

        [HttpPost("pay/{orderId}")]
        public async Task<IActionResult> Pay(int orderId)
        {
            try
            {
                await _IPaymentService.ProcessPaymentAsync(orderId);
                return Ok("Payment successful");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
