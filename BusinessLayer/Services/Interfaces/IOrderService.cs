using BusinessLayer.Dto;

namespace BusinessLayer.Services.Interfaces
{
    public interface IOrderService

    {
        Task<OrderDto> MakeOrderAsync(CartDto cart);

        Task<IEnumerable<OrderDto>> GetOrdersByUserAsync();
    }
}
