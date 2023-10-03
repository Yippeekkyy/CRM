using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCRM.Database;
using MyCRM.Model;

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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Waiter>>> GetWaiter()
        {
          if (_dbContext.Waiter == null)
          {
              return NotFound();
          }
            return await _dbContext.Waiter.ToListAsync();
        }

        // GET: api/Admin1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Waiter>> GetWaiter(int id)
        {
          if (_dbContext.Waiter == null)
          {
              return NotFound();
          }
            var waiter = await _dbContext.Waiter.FindAsync(id);

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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Waiter>> PostWaiter(Waiter waiter)
        {
          if (_dbContext.Waiter == null)
          {
              return Problem("Entity set 'MainDbContext.Waiter'  is null.");
          }
            _dbContext.Waiter.Add(waiter);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("GetWaiter", new { id = waiter.WaiterId }, waiter);
        }

        // DELETE: api/Admin1/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWaiter(int id)
        {
            if (_dbContext.Waiter == null)
            {
                return NotFound();
            }
            var waiter = await _dbContext.Waiter.FindAsync(id);
            if (waiter == null)
            {
                return NotFound();
            }

            _dbContext.Waiter.Remove(waiter);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool WaiterExists(int id)
        {
            return (_dbContext.Waiter?.Any(e => e.WaiterId == id)).GetValueOrDefault();
        }
    }
}
