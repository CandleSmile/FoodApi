using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessLayer.Dto
{
    public class CartItemDto
    {
        [JsonPropertyName("mealId")]
        [Required]
        public int MealId { get; set; }

        [JsonPropertyName("quantity")]
        [Required]
        public int Quantity { get; set; }

        [JsonPropertyName("price")]
        [Required]
        public decimal Price { get; set; }

        [JsonPropertyName("title")]
        [Required]
        public string Title { get; set; }

    }
}
