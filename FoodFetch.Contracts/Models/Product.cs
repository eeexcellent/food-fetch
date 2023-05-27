namespace FoodFetch.Contracts.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Calories { get; set; }
    }
}