using MyCRM.Model;

namespace MyCRM.Responses
{
    public class GetTableResponse
    {
        public int TableId { get; set; }
        public string Description { get; set; }

        public GetTableResponse(Table table)
        {
            TableId = table.Id;
            Description = table.Description;
        }
    }


}
