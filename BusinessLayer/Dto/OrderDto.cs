using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime DateCreated { get; set; }

        public bool IsPaid { get; set; }

        public bool IsSuccessful { get; set; }

        public List<OrderItemDto>? OrderItems { get; set; }
    }
}
