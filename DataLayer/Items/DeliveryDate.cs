using System.ComponentModel.DataAnnotations;

namespace DataLayer.Items
{
    public class DeliveryDate
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public List<TimeSlot> TimeSlots { get; set; } = new();

        public List<DeliveryDateTimeSlot> DeliverуDateTimeSlots { get; set; } = new();
    }
}
