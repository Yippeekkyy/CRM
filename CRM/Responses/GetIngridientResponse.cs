using MyCRM.Model;

namespace MyCRM.Responses
{
    public class GetIngridientResponse
    {
        public int IngridientId { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }

        public GetIngridientResponse(Ingridient ingridient)
        {
            IngridientId = ingridient.IngridientId;
            Name = ingridient.Name;
            Count = ingridient.Count;
        }
    }


}
