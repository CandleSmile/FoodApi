using DataLayer.Items;
using DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.Implementation
{
    public class DeliveryDateTimeSlotRepository : BaseRepository<DeliveryDateTimeSlot>, IDeliveryDateTimeSlotRepository
    {
        public DeliveryDateTimeSlotRepository(FoodContext context) : base(context)
        {
        }

        public async Task<IEnumerable<DeliveryDate>?> GetDeliveryDates(DateTime fromNow)
        {
            return await _context.DeliveryDates.Where(x => x.Date.Date >= fromNow.Date).Include(x => x.DeliverуDateTimeSlots)
                .Where(x => x.DeliverуDateTimeSlots.Any(y => (y.MaximumOrders == null || (y.MaximumOrders ?? 0 - y.MadeOrders ?? 0) > 0))).ToListAsync();
        }

        public async Task<IEnumerable<DeliveryDateTimeSlot>?> GetDeliveryTimeSlots(DateTime date)
        {
            return await _context.DeliveryDateTimeSlots.Include(x => x.DeliveryDate).Where(x => x.DeliveryDate.Date == date.Date && (x.MaximumOrders == null || (x.MaximumOrders ?? 0 - x.MadeOrders ?? 0) > 0)).Include(y => y.TimeSlot).ToListAsync();
        }

        public async Task<DeliveryDateTimeSlot?> GetDeliveryDateTimeSlotByIdAsync(int id)
        {
            return await _context.DeliveryDateTimeSlots.Include(x => x.DeliveryDate).Include(x => x.TimeSlot)
                .FirstAsync(x => x.Id == id);
        }
    }
}
