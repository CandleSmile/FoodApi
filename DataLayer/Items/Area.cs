using System.ComponentModel.DataAnnotations;


namespace DataLayer.Items
{
    public class Area
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Name { get; set; }
    }
}
