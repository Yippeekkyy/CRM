namespace MyCRM.Model
{
    public class Order
    {
        public int OrderId { get; set; }
        public Waiter Waiter { get; set; }
        public Table Table { get; set; }

        public DateTime OrderTime { get; set; }

        public List<Dish> Dishes { get; set; } = new();

    }
}
