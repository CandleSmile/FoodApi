using System.ComponentModel.DataAnnotations;

namespace DataLayer.Items
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }
    }
}
