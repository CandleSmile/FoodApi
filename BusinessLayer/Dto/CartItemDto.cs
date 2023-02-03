using System.Text.Json.Serialization;

namespace BusinessLayer.Dto
{
    public class CartItemDto
    {
        [JsonPropertyName("mealId")]
        public int MealId { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

    }
}
