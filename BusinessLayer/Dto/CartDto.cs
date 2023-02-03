using System.Text.Json.Serialization;

namespace BusinessLayer.Dto
{
    public class CartDto
    {
        [JsonPropertyName("cartItems")]
        public List<CartItemDto> CartItems { get; set; } = new List<CartItemDto>();
    }
}
