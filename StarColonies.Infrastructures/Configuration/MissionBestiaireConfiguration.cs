using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Infrastructures.Entities;

namespace StarColonies.Infrastructures.Configuration;

public class MissionBestiaireConfiguration : IEntityTypeConfiguration<MissionBestiaire>
{
    public void Configure(EntityTypeBuilder<MissionBestiaire> builder)
    {
        
        builder.HasKey(mb => mb.Id);
        
        builder.HasKey(br => new { br.IdMission, br.IdBestiaire });
        
        builder.HasOne(mb => mb.Mission)
            .WithMany(m => m.MissionBestiaires)
            .HasForeignKey(mb => mb.IdMission)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(mb => mb.Bestiaire)
            .WithMany(b => b.MissionBestiaires)
            .HasForeignKey(mb => mb.IdBestiaire)
            .OnDelete(DeleteBehavior.Cascade);
        
        SeedMissionRelations(builder);
    }

    private void SeedMissionRelations(EntityTypeBuilder<MissionBestiaire> builder)
    {
        var missionBestiaires = new List<MissionBestiaire>
        {
            new() { IdMission = 1, IdBestiaire = 1 },
            new() { IdMission = 1, IdBestiaire = 2 },
            new() { IdMission = 1, IdBestiaire = 3 },
            new() { IdMission = 1, IdBestiaire = 4 },
            
            new() { IdMission = 2, IdBestiaire = 5 },
            new() { IdMission = 2, IdBestiaire = 6 },
            new() { IdMission = 2, IdBestiaire = 7 },
            new() { IdMission = 2, IdBestiaire = 8 },
            
            new() { IdMission = 3, IdBestiaire = 9 },
            new() { IdMission = 3, IdBestiaire = 10 },
            new() { IdMission = 3, IdBestiaire = 11 },
            new() { IdMission = 3, IdBestiaire = 12 },
            
            new() { IdMission = 4, IdBestiaire = 13 },
            new() { IdMission = 4, IdBestiaire = 1 },
            new() { IdMission = 4, IdBestiaire = 2 },
            new() { IdMission = 4, IdBestiaire = 3 },
            new() { IdMission = 4, IdBestiaire = 4 },
            new() { IdMission = 4, IdBestiaire = 5 }
        };
        
        builder.HasData(missionBestiaires);
    }
}