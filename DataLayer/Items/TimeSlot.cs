using System.ComponentModel.DataAnnotations;

namespace DataLayer.Items
{
    public class TimeSlot
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Time { get; set; }

        public List<DeliveryDate> DeliveryDates { get; set; }

        public List<DeliveryDateTimeSlot> DeliverуDateTimeSlots { get; set; } = new();
    }
}
