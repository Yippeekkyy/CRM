using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemplateAspNet.Model;

namespace TemplateAspNet.Database.Configuration
{
    public class WaiterConfiguration : IEntityTypeConfiguration<Waiter>
    {
        public void Configure(EntityTypeBuilder<Waiter> builder)
        {
            builder.HasKey(i => i.WaiterId);

         //   builder.HasMany<WaiterSchedule>().WithOne(i => i.Waiter);

        }
    }
}
