using System.ComponentModel.DataAnnotations;

namespace DataLayer.Items
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Name { get; set; }

        public List<Meal>? Meals { get; set; } = new();
    }
}
