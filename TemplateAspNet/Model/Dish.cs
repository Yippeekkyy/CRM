namespace TemplateAspNet.Model;

public class Dish
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }

    public List<Order> Orders { get; set; } = new();
}