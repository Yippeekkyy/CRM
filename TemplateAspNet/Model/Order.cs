namespace TemplateAspNet.Model
{
    public class Order
    {
        public int Id { get; set; }
        public Waiter Waiter { get; set; }

        public DateTime OrderTime { get; set; }

        public List<Dish> Dishes { get; set; } = new();
    }
}
