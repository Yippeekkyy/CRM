using System.Reflection.Emit;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MyCRM.Model;

namespace MyCRM.Database
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        public DbSet<Waiter> Waiters { get; set; }
        public DbSet<WaiterSchedule> WaiterSchedules { get; set; }

        public DbSet<TableSchedule> TableSchedules { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Ingridient> Ingridients { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Category> Categories { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Assembly assemblyWithConfigurations = GetType().Assembly; //get whatever assembly you want
            modelBuilder.ApplyConfigurationsFromAssembly(assemblyWithConfigurations);
        }
    }
}
