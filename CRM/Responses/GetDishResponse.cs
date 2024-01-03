using MyCRM.Model;

namespace MyCRM.Responses
{
    public class GetDishResponse
    {
        public int DishId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        public GetDishResponse()
        {

        }

        public GetDishResponse(Dish dish)
        {
            DishId = dish.DishId;
            Name = dish.Name;
            Price = dish.Price;
        }
    }


}
