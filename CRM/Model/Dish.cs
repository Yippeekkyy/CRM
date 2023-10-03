namespace MyCRM.Model;

public class Dish
{
    public int DishId { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }

    public List<Order> Orders { get; set; } = new();
    public List<Ingridient> Ingridients { get; set; } = new();
    public List<Category> Categories { get; set; } = new();
}