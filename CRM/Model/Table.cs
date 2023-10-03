namespace MyCRM.Model
{
    public class Table
    {
        public int TableId { get; set; }
        public string? Description { get; set; }
        public List<TableSchedule> Schedules { get; set; } = new();
    }
}
