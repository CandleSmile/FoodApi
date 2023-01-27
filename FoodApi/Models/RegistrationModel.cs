
using System.ComponentModel.DataAnnotations;


namespace FoodApi.Models
{
    public  class RegistrationModel
    {     

        [Required]
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Must be between 6 and 16 characters", MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Must be between 6 and 16 characters", MinimumLength = 6)]
        [Compare("Password")]
        public string PasswordConfirmation { get; set; }
     
    }
}
