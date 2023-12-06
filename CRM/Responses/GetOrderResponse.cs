using MyCRM.Model;

namespace MyCRM.Responses
{
    public class GetOrderResponse 
    {
        
        public int Id { get; set; }
        public int WaiterId { get; set; }
        public int TableId { get; set; }
        public DateTime OrderTime { get; set; }

        public GetOrderResponse(Order order)
        {
            Id = order.OrderId;
            WaiterId = order.WaiterId;
            TableId = order.TableId;
            OrderTime = order.OrderTime;
        }
    }
}