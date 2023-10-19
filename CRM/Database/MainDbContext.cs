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
         //   Database.EnsureDeleted();
         //   Database.EnsureCreated();
            
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Waiter> Waiters { get; set; }
        public DbSet<WaiterSchedule> WaiterSchedules { get; set; }

        public DbSet<TableSchedule> TableSchedules { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Ingridient> Ingridients { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserRole> Roles { get; set; } 
        
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Assembly assemblyWithConfigurations = GetType().Assembly; //get whatever assembly you want
            modelBuilder.ApplyConfigurationsFromAssembly(assemblyWithConfigurations);

            modelBuilder.Entity<UserRole>().HasData
            (
                new UserRole() {Id = 1, Role = RoleType.Admin },
                new UserRole() {Id = 2,Role = RoleType.Waiter }
                
            );

            modelBuilder.Entity<User>().HasData
            (
                new User() { Id = 1, FirstName = "Александер", LastName = "Смирнов", Patronymic = "Владимирович",Phone = 123456, Password = 1111.ToString(),RoleId = 1},
                new User() { Id = 2, FirstName = "Василий", LastName = "Антонов", Patronymic = "Сергеевич",Phone = 11111, Password = 2222.ToString(),RoleId = 2}
                );
            
            

        }
    }
}
