namespace MyCRM.Model
{
    public class TableSchedule
    {
        public int Id { get; set; }
        public Table Table { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
