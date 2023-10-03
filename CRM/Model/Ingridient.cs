namespace MyCRM.Model
{
    public class Ingridient
    {
        public int IngridientId { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }

        public List<Dish> Dishes { get; set; } = new();
    }
}
