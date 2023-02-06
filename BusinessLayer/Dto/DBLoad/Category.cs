using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessLayer.Dto.DBLoad
{
    public class CategoryDb
    {
        [JsonPropertyName("strCategory")]
        [Required]
        public string? Name { get; set; }

        [JsonPropertyName("strCategoryDescription")]
        public string? Description { get; set; }

        [JsonPropertyName("strCategoryThumb")]
        [Required]
        public string? Image { get; set; }
    }
}
