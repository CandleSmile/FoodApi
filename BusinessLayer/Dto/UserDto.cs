using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Dto
{
    public class UserDto
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Must be between 6 and 16 characters", MinimumLength = 6)]
        public string Password { get; set; }

        public string? RefreshToken { get; set; }

    }
}
