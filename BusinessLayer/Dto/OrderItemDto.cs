using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Dto
{
    public class OrderItemDto
    {
        [Required]
        public int OrderId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public string Title { get; set; }
    }
}
