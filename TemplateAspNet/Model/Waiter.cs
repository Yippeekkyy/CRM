namespace TemplateAspNet.Model;

public class Waiter
{
    public int WaiterId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Patronimyc { get; set; }
    public int? Phone { get; set; }

    public List<WaiterSchedule> WaiterSchedules { get; set; } = new();
    public List<Order> Orders { get; set; } = new();
}