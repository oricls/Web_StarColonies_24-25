using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Infrastructures.Entities;

namespace StarColonies.Infrastructures.Configuration;

public class ResultatMissionConfiguration : IEntityTypeConfiguration<ResultatMission>
{
    public void Configure(EntityTypeBuilder<ResultatMission> builder)
    {
        builder.HasKey(rm => rm.Id);

        builder.Property(rm => rm.Issue)
            .IsRequired();

        builder.Property(rm => rm.Date)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(rm => rm.Mission)
            .WithMany(m => m.ResultatMissions)
            .HasForeignKey(rm => rm.IdMission)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(rm => rm.Team)
            .WithMany(t => t.ResultatMissions)
            .HasForeignKey(rm => rm.IdTeam)
            .OnDelete(DeleteBehavior.Cascade);
        
        SeedResultatMission(builder);
    }

    private void SeedResultatMission(EntityTypeBuilder<ResultatMission> builder)
    {
        builder.HasData();
    }
}