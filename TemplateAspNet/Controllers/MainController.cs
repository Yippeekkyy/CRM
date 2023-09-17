using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCRM.Database;
using MyCRM.Model;

namespace MyCRM.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MainController : ControllerBase
    {
        public readonly MainDbContext _dbContext;

        public MainController(MainDbContext DbContext) 
        {
            _dbContext = DbContext;
        }

        [HttpGet("Test")]
        public async Task<List<Waiter>> test()
        {
            await _dbContext.Waiter.AddAsync(new Waiter()
            {
                FirstName = "john",
                LastName = "Smith",
                Patronimyc = "ss",
                Phone = 12345,
               WaiterSchedules = new List<WaiterSchedule>()
            {
                new(){Start = DateTime.Now, End = DateTime.Now.AddHours(-5)},
                new(){Start = DateTime.Now, End = DateTime.Now.AddHours(-8)}
            }

            });

       
            
      

  
            return null;
        }

        [HttpGet("Test1")]
        public async Task<string> test1()
        {
            return "Hello World";
        }

    }
}
