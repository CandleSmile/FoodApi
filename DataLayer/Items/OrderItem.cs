namespace DataLayer.Items
{
    using System.ComponentModel.DataAnnotations;

    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        public Order? Order { get; set; }

        [Required]
        public int MealId { get; set; }

        public Meal? Meal { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public string Title { get; set; }

    }
}
