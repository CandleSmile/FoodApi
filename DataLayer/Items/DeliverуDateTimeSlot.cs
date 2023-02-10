using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Items
{
    public class DeliveryDateTimeSlot
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int DeliveryDateId { get; set; }

        public DeliveryDate DeliveryDate { get; set; }

        [Required]
        public int TimeSlotId { get; set; }

        public TimeSlot TimeSlot { get; set; }

        public int? MaximumOrders { get; set; }

        public int? MadeOrders { get; set; }

        [NotMapped]
        public bool IsAvailable
        {
            get
            {
                return MaximumOrders == null || (MaximumOrders ?? 0 - MadeOrders ?? 0) > 0;
            }
        }
    }
}
