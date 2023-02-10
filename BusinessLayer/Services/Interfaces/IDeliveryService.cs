using BusinessLayer.Dto;

namespace BusinessLayer.Services.Interfaces
{
    public interface IDeliveryService
    {
        Task<IEnumerable<DeliveryDateDto>?> GetDateAsync(DateTime fromDateTime);

        Task<IEnumerable<DeliveryTimeSlotDto>> GetTimeSlotsAsync(DateTime dateTime);
    }
}
