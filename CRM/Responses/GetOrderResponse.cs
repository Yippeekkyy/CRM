using MyCRM.Model;

namespace MyCRM.Responses
{
    public class GetOrderResponse 
    {
        
        
        public int WaiterId { get; set; }
        public int TableId { get; set; }
        public DateTime OrderTime { get; set; }

        public GetOrderResponse(Order order)
        {
            
            WaiterId = order.WaiterId;
            TableId = order.TableId;
            OrderTime = order.OrderTime;
        }
    }
}