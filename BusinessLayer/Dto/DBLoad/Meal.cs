using DataLayer.Items;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessLayer.Dto.DBLoad
{
    public class MealDb
    {
        [JsonPropertyName("idMeal")]
        public int IdMeal { get; set; }

        [JsonPropertyName("strMeal")]
        [Required]
        public string? Name { get; set; }

        [JsonPropertyName("strInstructions")]
        public string? Description { get; set; }

        [JsonPropertyName("strCategory")]
        [Required]
        public string? CategoryString { get; set; }

        [JsonPropertyName("strArea")]
        public string? AreaString { get; set; }

        [JsonPropertyName("strIngredients")]
        public List<string>? IngredientsStrings { get; set; }

        [JsonPropertyName("strTagsList")]
        public List<string>? TagStrings { get; set; }

        [JsonPropertyName("strMealThumb")]
        public string? Image { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonIgnore]
        public List<Tag>? Tags { get; set; }

        [JsonIgnore]
        public Category? Category { get; set; }

        [JsonIgnore]
        public List<Ingredient>? Ingredients { get; set; }

    }
}
