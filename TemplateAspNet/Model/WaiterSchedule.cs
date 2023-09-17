namespace MyCRM.Model
{
    public class WaiterSchedule
    {
        public int Id { get; set; }
      //  public int WaiterId { get; set; }
        public Waiter Waiter { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
