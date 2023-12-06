using MyCRM.Model;

namespace MyCRM.Responses
{
    public class GetCategoryResponse
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public GetCategoryResponse(Category category)
        {
            CategoryId = category.CategoryId; 
            Name = category.Name;
        }
    }
}