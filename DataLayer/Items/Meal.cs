namespace DataLayer.Items
{
    using System.ComponentModel.DataAnnotations;

    public class Meal
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public int AreaId { get; set; }

        public Area? Area { get; set; }

        public string? Image { get; set; }

        public List<Ingredient>? Ingredients { get; set; }

        public List<Tag>? Tags { get; set; }

    }
}
