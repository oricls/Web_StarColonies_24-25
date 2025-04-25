using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StarColonies.Infrastructures.Configuration;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    private static readonly string AdminId = Environment.MachineName+DateTime.Parse("2024-04-01T10:30:00.0")+"Admin";
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole
            {
                Id = AdminId,
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = AdminId
            }
        );
    }
}