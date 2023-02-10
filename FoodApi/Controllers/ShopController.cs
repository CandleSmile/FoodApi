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
        private readonly IDeliveryService _deliveryService;

        public ShopController(IOrderService orderService, IDeliveryService deliveryService)
        {
            _orderService = orderService;
            _deliveryService = deliveryService;
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

        [HttpGet("GetDeliveryDates")]
        public async Task<ActionResult<IEnumerable<DeliveryDateDto>>> GetDates(DateTime fromDate)
        {
            var dates = await _deliveryService.GetDateAsync(fromDate);
            return Ok(dates ?? new List<DeliveryDateDto>());
        }


        [HttpGet("GetDeliveryTimeSlots")]
        public async Task<ActionResult<IEnumerable<DeliveryTimeSlotDto>>> GetTimeSlots(DateTime date)
        {
            var times = await _deliveryService.GetTimeSlotsAsync(date);
            return Ok(times ?? new List<DeliveryTimeSlotDto>());
        }
    }
}
