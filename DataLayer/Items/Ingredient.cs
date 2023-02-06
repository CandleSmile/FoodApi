namespace DataLayer.Items
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Ingredient
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Name { get; set; }

        public string? Description { get; set; }

        public List<Meal>? Meals { get; set; } = new();
    }
}
