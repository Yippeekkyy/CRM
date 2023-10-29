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
            waiterToUpdate.Patronymic = waiter.Patronymic;
            waiterToUpdate.Phone = waiter.Phone;

            await _dbContext.SaveChangesAsync();
          

            var response = new GetWaiterResponse(waiterToUpdate);
            return response;
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> PostUser(AddWaiterRequest request)
        {
            var waiter = new User()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Patronymic = request.Patronymic,
                Phone = request.Phone,
                RoleId = 2
            };
            var waiterEntity = await _dbContext.Users.AddAsync(waiter);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("Waiter")]
        public async Task<IActionResult> PostWaiter(AddWaiterRequest request)
        {
          var waiter = new Waiter()
          {
              FirstName = request.FirstName,
              LastName = request.LastName,
              Patronymic = request.Patronymic,
              Phone = request.Phone,
              RoleId = 2
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

        //TableController

        [HttpGet("Tables")]
        public async Task<ActionResult<IEnumerable<GetTableResponse>>> GetTables()
        {
            if (_dbContext.Tables == null)
            {
                return NotFound();
            }

            var tables = await _dbContext.Tables.ToListAsync();

            var response = new List<GetTableResponse>();

            tables.ForEach(i => response.Add(new GetTableResponse(i)));

            return response;
        }


        [HttpGet("Table/{id}")]
        public async Task<ActionResult<Table>> GetTable(int id)
        {
            if (_dbContext.Tables == null)
            {
                return NotFound();
            }
            var table = await _dbContext.Tables.FindAsync(id);

            if (table == null)
            {
                return NotFound();
            }

            return table;
        }


        [HttpPut("Table/{id}")]
        public async Task<GetTableResponse> EditTable(int id, [FromBody] EditTableRequest table)
        {
            var tableToUpdate = await _dbContext.Tables.FindAsync(id);
            tableToUpdate.Description = table.Description;
            


            await _dbContext.SaveChangesAsync();


            var response = new GetTableResponse(tableToUpdate);
            return response;
        }


        [HttpPost("Table")]
        public async Task<ActionResult<Table>> PostTable(AddTableRequest request)
        {
            if (_dbContext.Tables == null)
            {
                return Problem("Entity set 'MainDbContext.Tables'  is null.");
            }

            var table = new Table()
            {
                Description = request.Description,
                
            };

            await _dbContext.Tables.AddAsync(table);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("GetTable", new { id = table.Id }, table);
        }


        [HttpDelete("Table/{id}")]
        public async Task<IActionResult> DeleteTable(int id)
        {
            if (_dbContext.Tables == null)
            {
                return NotFound();
            }
            var table = await _dbContext.Tables.FindAsync(id);
            if (table == null)
            {
                return NotFound();
            }

            _dbContext.Tables.Remove(table);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
