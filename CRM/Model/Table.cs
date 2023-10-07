using System.Text.Json.Serialization;

namespace MyCRM.Model
{
    public class Table
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        [JsonIgnore]
        public List<TableSchedule> Schedules { get; set; } = new();
    }
}
