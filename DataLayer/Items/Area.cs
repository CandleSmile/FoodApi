namespace DataLayer.Items
{
    using System.ComponentModel.DataAnnotations;

    public class Area
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Name { get; set; }
    }
}
