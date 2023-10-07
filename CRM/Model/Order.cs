using System.ComponentModel.DataAnnotations;

using System.Text.Json.Serialization;


namespace MyCRM.Model
{
    public class Order
    {
        public int OrderId { get; set; }
        public int WaiterId { get; set; } // внешний ключ
        [JsonIgnore]
        public Waiter? Waiter { get; set; } // навигационное свойство
        public int TableId { get; set; }
        [JsonIgnore]
        public Table? Table { get; set; }

        public DateTime OrderTime { get; set; }
        [JsonIgnore]
        public List<Dish> Dishes { get; set; } = new();

    }
}
