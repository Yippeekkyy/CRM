namespace MyCRM.Requests
{
    public class EditOrderRequest
    {
        public int OrderId { get; set; }
        public int WaiterId { get; set; }
        public int TableId { get; set; }
        
    }
}