using System.Text.Json.Serialization;

namespace MyCRM.Model;

public class Dish
{
    public int DishId { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    [JsonIgnore]
    public List<Order> Orders { get; set; } = new();
    [JsonIgnore]
    public List<Ingridient> Ingridients { get; set; } = new();
    [JsonIgnore]
    public List<Category> Categories { get; set; } = new();
}