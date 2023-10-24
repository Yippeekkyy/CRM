namespace MyCRM.Requests
{
    public class AddOrderRequest
    {
        public int WaiterId { get; set; }
        public int TableId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
