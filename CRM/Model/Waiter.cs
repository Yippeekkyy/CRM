using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyCRM.Model
{
    public class Waiter: User
    {
        
        [JsonIgnore] public List<WaiterSchedule> WaiterSchedules { get; set; } = new();
        [JsonIgnore] public List<Order> Orders { get; set; } = new();

    }
}