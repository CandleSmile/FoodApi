using DataLayer.Items;

namespace DataLayer.Repositories.Interfaces
{
    public interface IDeliveryDateTimeSlotRepository : IBaseRepository<DeliveryDateTimeSlot>
    {
        Task<IEnumerable<DeliveryDate>?> GetDeliveryDates(DateTime fromNow);

        Task<IEnumerable<DeliveryDateTimeSlot>?> GetDeliveryTimeSlots(DateTime date);

        Task<DeliveryDateTimeSlot?> GetDeliveryDateTimeSlotByIdAsync(int id);
    }
}
