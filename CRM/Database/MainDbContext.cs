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
            Database.EnsureCreated();
        }

        public DbSet<Waiter> Waiter { get; set; }
        public DbSet<WaiterSchedule> WaiterSchedule { get; set; }

        public DbSet<TableSchedule> TableSchedule { get; set; }
        public DbSet<Table> Table { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Ingridient> Ingridient { get; set; }
        public DbSet<Dish> Dish { get; set; }
        public DbSet<Category> Category { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Assembly assemblyWithConfigurations = GetType().Assembly; //get whatever assembly you want
            modelBuilder.ApplyConfigurationsFromAssembly(assemblyWithConfigurations);
        }
    }
}
