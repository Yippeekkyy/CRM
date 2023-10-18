﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class AdminController : ControllerBase
    {
        private readonly MainDbContext _dbContext;

        public AdminController(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [HttpGet("Waiters")]
        public async Task<ActionResult<IEnumerable<GetWaiterResponse>>> GetWaiters()
        {
          if (_dbContext.Waiters == null)
          {
              return NotFound();
          }

          var waiters = await _dbContext.Waiters.ToListAsync();


          var response = new List<GetWaiterResponse>();

          waiters.ForEach(i => response.Add(new GetWaiterResponse(i)));
          
          return response;
        }
        
        [HttpGet("Waiter/{id}")]
        public async Task<ActionResult<Waiter>> GetWaiter(int id)
        {
            var waiter = await _dbContext.Waiters.FindAsync(id);

            if (waiter == null)
            {
                return NotFound();
            }

            return waiter;
        }
        
        [HttpPut("Waiter/{id}")]
        public async Task<GetWaiterResponse> EditWaiter(int id, [FromBody]EditWaiterRequest waiter) // ToDo Переписать, метод должен принимать только id
        {
            var waiterToUpdate = await _dbContext.Waiters.FindAsync(id);
            waiterToUpdate.FirstName = waiter.FirstName;
            waiterToUpdate.LastName = waiter.LastName;
            waiterToUpdate.Patronimyc = waiter.Patronymic;
            waiterToUpdate.Phone = waiter.Phone;
            
            await _dbContext.SaveChangesAsync();


            var response = new GetWaiterResponse(waiterToUpdate);
            return response;

        }

        [HttpPost("Waiter")]
        public async Task<IActionResult> PostWaiter(AddWaiterRequest request)
        {
          var waiter = new Waiter()
          {
              FirstName = request.FirstName,
              LastName = request.LastName,
              Patronimyc = request.Patronymic,
              Phone = request.Phone
          };
            var waiterEntity = await _dbContext.Waiters.AddAsync(waiter);
            await _dbContext.SaveChangesAsync();

            var response = new GetWaiterResponse(waiterEntity.Entity);
            
            return Ok(response);
        }
        
        [HttpDelete("Waiter/{id}")]
        public async Task<IActionResult> DeleteWaiter(int id)
        {
            if (_dbContext.Waiters == null)
            {
                return NotFound();
            }
            var waiter = await _dbContext.Waiters.FindAsync(id);
            if (waiter == null)
            {
                return NotFound();
            }

            _dbContext.Waiters.Remove(waiter);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
        
        
        
        //Dishes
        
        [HttpGet("Dishes")]
        public async Task<ActionResult<IEnumerable<GetDishesResponse>>> GetDishes()
        {
            if (_dbContext.Dishes == null)
            {
                return NotFound();
            }

            var dishes = await _dbContext.Dishes.ToListAsync();

            var response = new List<GetDishesResponse>();

            dishes.ForEach(i => response.Add(new GetDishesResponse(i)));

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
        public async Task<IActionResult> PutDish(int id, Dish dish)
        {
            if (id != dish.DishId)
            {
                return BadRequest();
            }

            _dbContext.Entry(dish).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DishExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
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

        
        
        
        private bool DishExists(int id)
        {
            return (_dbContext.Dishes?.Any(e => e.DishId == id)).GetValueOrDefault();
        }
        
        private bool WaiterExists(int id)
        {
            return (_dbContext.Waiters?.Any(e => e.WaiterId == id)).GetValueOrDefault();
        }
        
    }
}
