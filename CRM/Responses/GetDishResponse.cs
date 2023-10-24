using MyCRM.Model;

namespace MyCRM.Responses
{
    public class GetDishesResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        public GetDishesResponse()
        {

        }

        public GetDishesResponse(Dish dish)
        {
            Id = dish.DishId;
            Name = dish.Name;
            Price = dish.Price;
        }
    }


}
