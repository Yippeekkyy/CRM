using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemplateAspNet.Model;

namespace TemplateAspNet.Database.Configuration
{
    public class WaiterScheduleConfiguration : IEntityTypeConfiguration<WaiterSchedule>
    {
        public void Configure(EntityTypeBuilder<WaiterSchedule> builder)
        {
            builder.HasKey(i => i.Id);

            builder.HasIndex(i => i.Start);
            builder.HasIndex(i => i.End);
        }
    }
}
