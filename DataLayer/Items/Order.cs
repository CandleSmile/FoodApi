using System.ComponentModel.DataAnnotations;

namespace DataLayer.Items
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        public User User { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime DateCreated { get; set; }

        public bool IsPaid { get; set; }

        public bool IsSuccessful { get; set; }

        public DateTime DeliveryDate { get; set; }

        public string TimeSlot { get; set; }

        public List<OrderItem>? OrderItems { get; set; } = new List<OrderItem>();

    }
}
