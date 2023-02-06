namespace BusinessLayer.Dto
{
    public class MealsFilterParams
    {
        public string? SearchString { get; set; }

        public int? CategoryId { get; set; }

        public List<int>? IngredientIds { get; set; }
    }
}
