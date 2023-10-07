using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCRM.Model;

namespace MyCRM.Database.Configuration
{
    public class WaiterScheduleConfiguration : IEntityTypeConfiguration<WaiterSchedule>
    {
        public void Configure(EntityTypeBuilder<WaiterSchedule> builder)
        {
            builder.HasKey(i => i.WaiterScheduleId);

            builder.HasIndex(i => i.Start);
            builder.HasIndex(i => i.End);
        }
    }
}
