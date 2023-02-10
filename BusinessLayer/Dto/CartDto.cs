using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessLayer.Dto
{
    public class CartDto
    {
        [JsonPropertyName("cartItems")]
        [Required]
        public List<CartItemDto> CartItems { get; set; } = new List<CartItemDto>();

        [Required]
        public int DeliveryDateTimeSlotId { get; set; }
    }
}
