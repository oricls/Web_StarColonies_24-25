using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Infrastructures.Entities;

namespace StarColonies.Infrastructures.Configuration;

public class ActivityLogConfiguration : IEntityTypeConfiguration<ActivityLog>
{
    public void Configure(EntityTypeBuilder<ActivityLog> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasMany<Log>()
            .WithOne()
            .HasForeignKey(x => x.ActivityLogId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany<Admin>()
            .WithOne()
            .HasForeignKey(x => x.ActivityLogId)
            .OnDelete(DeleteBehavior.Cascade);
        
        SeedActivityLogs(builder);
    }

    private void SeedActivityLogs(EntityTypeBuilder<ActivityLog> builder)
    {
        //builder.HasData();
    }
}