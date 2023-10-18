using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCRM.Model;

namespace MyCRM.Database.Configuration;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(t => t.Role)
            .HasConversion<string>(s => s.DisplayName(), s => EnumHelper.FromDisplayName<RoleType>(s));
    }
}