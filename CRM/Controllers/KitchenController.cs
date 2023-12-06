using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MyCRM.Database;
using MyCRM.Model;
using MyCRM.Requests;
using MyCRM.Responses;


namespace MyCRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KitchenController : ControllerBase
    {
        private readonly MainDbContext _dbContext;

        public KitchenController(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("Category")]
        public async Task<ActionResult<IEnumerable<GetCategoryResponse>>> GetCategories()
        {
            if (_dbContext.Categories == null)
            {
                return NotFound();
            }

            var categories = await _dbContext.Categories.ToListAsync();
            var response = new List<GetCategoryResponse>();
            categories.ForEach(i => response.Add(new GetCategoryResponse(i)));
            return response;
        }

        [HttpGet("Category/{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _dbContext.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        [HttpPut("Category/{id}")]
        public async Task<GetCategoryResponse> EditCategory(int id, [FromBody] EditCategoryRequest category) // ToDo Переписать, метод должен принимать только id
        {
            var categoryToUpdate = await _dbContext.Categories.FindAsync(id);
            categoryToUpdate.Name = category.Name;


            await _dbContext.SaveChangesAsync();


            var response = new GetCategoryResponse(categoryToUpdate);
            return response;
        }

        [HttpPost("AddCategory")]
        public async Task<IActionResult> PostCategory(AddCategoryRequest request)
        {
            var category = new Category()
            {
                Name = request.Name
            };
            var categoryEntity = await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("Category/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (_dbContext.Categories == null)
            {
                return NotFound();
            }
            var category = await _dbContext.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
        //Dishes

        [HttpGet("Dishes")]
        public async Task<ActionResult<IEnumerable<GetDishResponse>>> GetDishes()
        {
            if (_dbContext.Dishes == null)
            {
                return NotFound();
            }

            var dishes = await _dbContext.Dishes.ToListAsync();

            var response = new List<GetDishResponse>();

            dishes.ForEach(i => response.Add(new GetDishResponse(i)));

            return response;
        }


        [HttpGet("Dish/{id}")]
        public async Task<ActionResult<Dish>> GetDish(int id)
        {
            if (_dbContext.Dishes == null)
            {
                return NotFound();
            }
            var dish = await _dbContext.Dishes.FindAsync(id);

            if (dish == null)
            {
                return NotFound();
            }

            return dish;
        }


        [HttpPut("Dish/{id}")]
        public async Task<GetDishResponse> EditDish(int id, [FromBody] EditDishRequest dish)
        {
            var dishToUpdate = await _dbContext.Dishes.FindAsync(id);
            dishToUpdate.Name = dish.Name;
            dishToUpdate.Price = dish.Price;


            await _dbContext.SaveChangesAsync();


            var response = new GetDishResponse(dishToUpdate);
            return response;
        }


        [HttpPost("Dish")]
        public async Task<ActionResult<Dish>> PostDish(AddDishRequest request)
        {
            if (_dbContext.Dishes == null)
            {
                return Problem("Entity set 'MainDbContext.Dishes'  is null.");
            }

            var dish = new Dish()
            {
                Name = request.Name,
                Price = request.Price
            };

            await _dbContext.Dishes.AddAsync(dish);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("GetDish", new { id = dish.DishId }, dish);
        }


        [HttpDelete("Dish/{id}")]
        public async Task<IActionResult> DeleteDish(int id)
        {
            if (_dbContext.Dishes == null)
            {
                return NotFound();
            }
            var dish = await _dbContext.Dishes.FindAsync(id);
            if (dish == null)
            {
                return NotFound();
            }

            _dbContext.Dishes.Remove(dish);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        //Ingridients
        

        [HttpGet("Ingridients")]
        public async Task<ActionResult<IEnumerable<GetIngridientResponse>>> GetIngridients()
        {
            if (_dbContext.Ingridients == null)
            {
                return NotFound();
            }

            var ingridients = await _dbContext.Ingridients.ToListAsync();

            var response = new List<GetIngridientResponse>();

            ingridients.ForEach(i => response.Add(new GetIngridientResponse(i)));

            return response;
        }


        [HttpGet("Ingridient/{id}")]
        public async Task<ActionResult<Ingridient>> GetIngridient(int id)
        {
            if (_dbContext.Ingridients == null)
            {
                return NotFound();
            }
            var ingridient = await _dbContext.Ingridients.FindAsync(id);

            if (ingridient == null)
            {
                return NotFound();
            }

            return ingridient;
        }


        [HttpPut("Ingridient/{id}")]
        public async Task<GetIngridientResponse> EditIngridient(int id, [FromBody] EditIngridientRequest ingridient)
        {
            var ingridientToUpdate = await _dbContext.Ingridients.FindAsync(id);
            ingridientToUpdate.Name = ingridient.Name;
            ingridientToUpdate.Count = ingridient.Count;


            await _dbContext.SaveChangesAsync();


            var response = new GetIngridientResponse(ingridientToUpdate);
            return response;
        }


        [HttpPost("Ingridient")]
        public async Task<ActionResult<Ingridient>> PostIngridient(AddIngridientRequest request)
        {
            if (_dbContext.Ingridients == null)
            {
                return Problem("Entity set 'MainDbContext.Ingridients'  is null.");
            }

            var ingridient = new Ingridient()
            {
                Name = request.Name,
                Count = request.Count
            };

            await _dbContext.Ingridients.AddAsync(ingridient);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("GetIngridient", new { id = ingridient.IngridientId }, ingridient);
        }


        [HttpDelete("Ingridient/{id}")]
        public async Task<IActionResult> DeleteIngridient(int id)
        {
            if (_dbContext.Ingridients == null)
            {
                return NotFound();
            }
            var ingridient = await _dbContext.Ingridients.FindAsync(id);
            if (ingridient == null)
            {
                return NotFound();
            }

            _dbContext.Ingridients.Remove(ingridient);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

    }
}
