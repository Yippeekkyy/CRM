using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyCRM.Model;

public class Waiter
{
    [Key]
    public int WaiterId { get; set; }
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Patronimyc { get; set; }
    public int? Phone { get; set; }

    [JsonIgnore]
    public List<WaiterSchedule> WaiterSchedules { get; set; } = new();
    [JsonIgnore]
    public List<Order> Orders { get; set; } = new();

}