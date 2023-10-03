using System.Collections.Generic;

namespace Client.Resources;



public class HotDish
{
    public string? Name { get; set; }
    public int Price { get; set; }
    public string? Description { get; set; }
    public List<HotDish> HotDishes { get; set; } = new()
    {
        new HotDish { Name = "Cocicka", Price = 20, Description = "OchenVkusno" },
        new HotDish { Name = "Kotletka", Price = 20, Description = "OchenVkusno" },
        new HotDish { Name = "Pureshka", Price = 20, Description = "OchenVkusno" },
        new HotDish { Name = "Makaroshki", Price = 20, Description = "OchenVkusno" }
        
    };
}