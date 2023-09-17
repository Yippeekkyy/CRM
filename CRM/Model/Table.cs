namespace MyCRM.Model
{
    public class Table
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public List<TableSchedule> Schedules { get; set; } = new();
    }
}
