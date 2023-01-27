using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Items
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string? Username { get; set; }

        [Required]
        public byte[]? PasswordHash { get; set; }

        [Required]
        public byte[]? PasswordSalt { get; set; }    
        
        public string? RefreshToken { get; set; }


    }
}
