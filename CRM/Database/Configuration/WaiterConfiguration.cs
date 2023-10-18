using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCRM.Model;

namespace MyCRM.Database.Configuration
{
    public class WaiterConfiguration : IEntityTypeConfiguration<Waiter>
    {
        public void Configure(EntityTypeBuilder<Waiter> builder)
        {
          //  builder.HasKey(i => i.WaiterId);

         //   builder.HasMany<WaiterSchedule>().WithOne(i => i.Waiter);

        }
    }
}
