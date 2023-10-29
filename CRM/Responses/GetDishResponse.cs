using MyCRM.Model;

namespace MyCRM.Responses
{
    public class GetDishResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        public GetDishResponse()
        {

        }

        public GetDishResponse(Dish dish)
        {
            Id = dish.DishId;
            Name = dish.Name;
            Price = dish.Price;
        }
    }


}
