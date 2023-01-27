using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessLayer.Contracts.DBLoad
{
    public class AreaDb
    {        

        [JsonPropertyName("strArea")]
        [Required]
        public string? Name { get; set; }
    }
}
