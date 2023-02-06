using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Dto
{
    public class AreaDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Name { get; set; }
    }
}
