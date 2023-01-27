using System.ComponentModel.DataAnnotations;

namespace FoodApi.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Must be between 6 and 16 characters", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
