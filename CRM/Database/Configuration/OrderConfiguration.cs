using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCRM.Model;

namespace MyCRM.Database.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder) 
        {
            builder.HasKey(i =>  i.OrderId);

            builder.HasIndex(i => i.OrderTime);
        }
    }
}
