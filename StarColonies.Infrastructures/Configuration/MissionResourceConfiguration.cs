using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Infrastructures.Entities;

namespace StarColonies.Infrastructures.Configuration;
public class MissionResourceConfiguration : IEntityTypeConfiguration<MissionResource>
{
    public void Configure(EntityTypeBuilder<MissionResource> builder)
    {
        builder.HasKey(mr => mr.Id);

        builder.HasOne(mr => mr.Mission)
            .WithMany(m => m.GainedResources)
            .HasForeignKey(mr => mr.IdMission)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(mr => mr.Resource)
            .WithMany(r => r.MissionResources)
            .HasForeignKey(mr => mr.IdResource)
            .OnDelete(DeleteBehavior.Cascade);
        
        SeedData(builder);
    }

    private void SeedData(EntityTypeBuilder<MissionResource> builder)
    {
        builder.HasData(
            new MissionResource { Id = 1, IdMission = 1, IdResource = 1 },
            new MissionResource { Id = 2, IdMission = 1, IdResource = 2 },
            new MissionResource { Id = 3, IdMission = 2, IdResource = 3, },
            new MissionResource { Id = 4, IdMission = 2, IdResource = 4 },
            new MissionResource { Id = 5, IdMission = 3, IdResource = 5 },
            new MissionResource { Id = 6, IdMission = 3, IdResource = 6 },
            new MissionResource { Id = 7, IdMission = 4, IdResource = 7 },
            new MissionResource { Id = 8, IdMission = 4, IdResource = 8 },
            new MissionResource { Id = 9, IdMission = 5, IdResource = 9 },
            new MissionResource { Id = 10, IdMission = 5, IdResource = 10 }
        );
    }
}