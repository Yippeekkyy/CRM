using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCRM.Database;
using MyCRM.Model;
using MyCRM.Requests;
using MyCRM.Responses;

namespace MyCRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Admin1Controller : ControllerBase
    {
        private readonly MainDbContext _dbContext;

        public Admin1Controller(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Admin1
        [HttpGet("GetWaiters")]
        public async Task<ActionResult<IEnumerable<GetWaitersResponse>>> GetWaiters()
        {
          if (_dbContext.Waiters == null)
          {
              return NotFound();
          }

          var waiters = await _dbContext.Waiters.ToListAsync();


          var response = new List<GetWaitersResponse>();

          waiters.ForEach(i => response.Add(new GetWaitersResponse(i)));
          
          return response;
        }

        // GET: api/Admin1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Waiter>> GetWaiter(int id)
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

            return waiter;
        }

        // PUT: api/Admin1/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWaiter(int id, Waiter waiter)
        {
            if (id != waiter.WaiterId)
            {
                return BadRequest();
            }

            _dbContext.Entry(waiter).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WaiterExists(id))
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

        // POST: api/Admin1
        [HttpPost("CreateWaiter")]
        public async Task<ActionResult<Waiter>> PostWaiter(AddWaiterRequest request)
        {
          if (_dbContext.Waiters == null)
          {
              return Problem("Entity set 'MainDbContext.Waiter'  is null.");
          }

          var waiter = new Waiter()
          {
              FirstName = request.FirstName,
              LastName = request.LastName,
              Patronimyc = request.Patronymic,
              Phone = request.Phone
          };
            await _dbContext.Waiters.AddAsync(waiter);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("GetWaiter", new { id = waiter.WaiterId }, waiter);
        }

        // DELETE: api/Admin1/5
        [HttpDelete("DeleteWaiter/{id}")]
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

        private bool WaiterExists(int id)
        {
            return (_dbContext.Waiters?.Any(e => e.WaiterId == id)).GetValueOrDefault();
        }
    }
}
