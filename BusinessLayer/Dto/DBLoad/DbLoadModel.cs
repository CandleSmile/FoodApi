using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessLayer.Dto.DBLoad

{
    public class DbLoadModel
    {
        
        [JsonPropertyName("categories")]
        [Required]
        public List<CategoryDb>? Categories { get; set; }

        [JsonPropertyName("areas")]
        [Required]
        public List<AreaDb>? Areas { get; set; }

        [JsonPropertyName("ingredients")]
        [Required]
        public List<IngredientDb>? Ingredients { get; set; }


        [JsonPropertyName("meals")]
        [Required]
        public List<MealDb>? Meals { get; set; }

    }
}
