namespace FoodApi.Models
{
    public class MealsFilterParams
    {
        public string? SearchString { get; set; }

        public int? CategoryId { get; set; }

        public List<int>? IdsIngredients { get; set; }
    }
}
