using DataLayer.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Contracts
{
    public class MealDto
    {
     
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public int AreaId { get; set; }

        public string? Image { get; set; }
    }
}
