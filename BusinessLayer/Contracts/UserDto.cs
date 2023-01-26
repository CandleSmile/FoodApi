using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Contracts
{
    public class UserDto
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
        public string? RefreshToken { get; set; }

    }
}
