using BusinessLayer.Dto;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace FoodApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowFoodApp")]
    [Authorize]
    public class ShopController : Controller
    {
        private readonly IOrderService _orderService;

        public ShopController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("MakeOrder")]
        public async Task<ActionResult<OrderDto>> MakeOrder(CartDto cart)
        {
            var order = await _orderService.MakeOrderAsync(cart);
            return Ok(order);
        }

        [HttpGet("GetOrders")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            var orders = await _orderService.GetOrdersByUserAsync();
            return Ok(orders);
        }
    }
}
