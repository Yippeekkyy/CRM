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
    public class WaiterController : ControllerBase
    {
        private readonly MainDbContext _dbContext;

        public WaiterController(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Waiter
        [HttpGet("Orders")]
        public async Task<ActionResult<IEnumerable<GetOrderResponse>>> GetOrders()
        {
            if (_dbContext.Orders == null)
            {
                return NotFound();
            }
            var orders = await _dbContext.Orders.ToListAsync();

            var response = new List<GetOrderResponse>();

            orders.ForEach(i => response.Add(new GetOrderResponse(i)));

            return response;
        }

        // GET: api/Waiter/5
        [HttpGet("Order/{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _dbContext.Orders.FindAsync(id);
            if (order == null) 
            {
                return NotFound();
            }
            return order;
        }

        
        [HttpPut("Order/{id}")]
        public async Task<GetOrderResponse> EditOrder(int id, [FromBody] EditOrderRequest order)
        {
            var orderToUpdate = await _dbContext.Orders.FindAsync(id);
            orderToUpdate.WaiterId = order.WaiterId;
            orderToUpdate.TableId = order.TableId;
            

            await _dbContext.SaveChangesAsync();

            var response = new GetOrderResponse(orderToUpdate);
            return response;
        }

        // POST: api/Waiter
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("CreateOrder")]
        public async Task<IActionResult> PostOrder(AddOrderRequest request)
        {
            var order = new Order()
            {
                WaiterId = request.WaiterId,
                TableId = request.TableId,
                OrderTime = DateTime.Now
            };
            var orderEntity = await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            var response = new GetOrderResponse(orderEntity.Entity);

            return Ok(response);
        }

        // DELETE: api/Waiter/5
        [HttpDelete("Order/{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (_dbContext.Orders == null)
            {
                return NotFound();
            }
            var order = await _dbContext.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        
    }
}
