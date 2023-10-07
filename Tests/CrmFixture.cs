using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using MyCRM;
using MyCRM.Database;


namespace Tests;


public class CrmFixture : WebApplicationFactory<Startup>
{
    public IServiceProvider ServiceScope { get; set; }
    public bool _disposed;
    
    public CrmFixture()
    {
        var scope = Services.CreateScope();
        ServiceScope = scope.ServiceProvider;
    }


    protected override void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            var context = Services.CreateScope().ServiceProvider.GetService<MainDbContext>();
            ClearDb(context);
        }

        _disposed = true;
    }

    private static void ClearDb(MainDbContext? dbContext)
    {
        dbContext.Waiters.RemoveRange(dbContext.Waiters);
        dbContext.Orders.RemoveRange(dbContext.Orders);
        dbContext.Dishes.RemoveRange(dbContext.Dishes);
        dbContext.Tables.RemoveRange(dbContext.Tables);
        dbContext.Ingridients.RemoveRange(dbContext.Ingridients);
        dbContext.Categories.RemoveRange(dbContext.Categories);
        dbContext.WaiterSchedules.RemoveRange(dbContext.WaiterSchedules);
        dbContext.SaveChanges();
    }
}