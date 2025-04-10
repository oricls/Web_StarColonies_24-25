using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Infrastructures.Entities;

namespace StarColonies.Infrastructures.Configuration;

public class MissionConfiguration : IEntityTypeConfiguration<Mission>
{
    public void Configure(EntityTypeBuilder<Mission> builder)
    {
        builder.HasKey(m => m.Id);
        
        builder.Property(m => m.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(m => m.Image)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(m => m.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasMany(m => m.ResultatMissions)
            .WithOne()
            .HasForeignKey(rm => rm.IdMission)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(m => m.Bestiaires)
            .WithMany(b => b.Missions)
            .UsingEntity(j => j.ToTable("MissionBestiaire"));

        SeedMissions(builder);
    }

    private void SeedMissions(EntityTypeBuilder<Mission> builder)
    {
        var missions = new List<Mission>
        {
            new()
            {
                Id = 1,
                Name = "Exploration de la base abandonnée",
                Image = "base_abandonnee.jpg",
                Description =
                    "Une ancienne base de recherche abandonnée recèle des créatures étranges et des technologies perdues."
            },
            new()
            {
                Id = 2,
                Name = "Chasse au Léviathan des abysses",
                Image = "leviathan_abysses.jpg",
                Description =
                    "Un monstre marin légendaire menace les colonies côtières. Son élimination est primordiale."
            },
            new()
            {
                Id = 3,
                Name = "Sauvetage dans la zone de quarantaine",
                Image = "quarantaine_zone.jpg",
                Description = "Des scientifiques sont piégés dans une zone contaminée par des créatures expérimentales."
            },
            new()
            {
                Id = 4,
                Name = "Désactivation du Hégémon",
                Image = "hegemon_reactor.jpg",
                Description = "Une entité extraterrestre intelligente a pris le contrôle d'un réacteur nucléaire."
            },
            new()
            {
                Id = 5,
                Name = "Nettoyage des ruines aliennes",
                Image = "ruines_aliennes.jpg",
                Description =
                    "Des créatures extraterrestres ont infesté d'anciennes ruines découvertes sur une lune lointaine."
            }
        };

        builder.HasData(missions);
    }
    
    public static void SeedMissionRelations(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Mission>()
            .HasMany(m => m.Bestiaires)
            .WithMany(b => b.Missions)
            .UsingEntity<Dictionary<string, object>>("MissionBestiaire",
                j => j.HasOne<Bestiaire>().WithMany().HasForeignKey("BestiaireId"),
                j => j.HasOne<Mission>().WithMany().HasForeignKey("MissionId"))
            .HasData(
                new { MissionId = 1, BestiaireId = 1 },
                new { MissionId = 1, BestiaireId = 2 },
                new { MissionId = 1, BestiaireId = 3 },
                new { MissionId = 1, BestiaireId = 4 },
                
                new { MissionId = 2, BestiaireId = 5 },
                new { MissionId = 2, BestiaireId = 6 },
                new { MissionId = 2, BestiaireId = 7 },
                new { MissionId = 2, BestiaireId = 8 },
                
                new { MissionId = 3, BestiaireId = 9 },
                new { MissionId = 3, BestiaireId = 10 },
                new { MissionId = 3, BestiaireId = 11 },
                new { MissionId = 3, BestiaireId = 12 },
                
                new { MissionId = 4, BestiaireId = 13 },
                new { MissionId = 4, BestiaireId = 1 },
                new { MissionId = 4, BestiaireId = 2 },
                new { MissionId = 4, BestiaireId = 3 },
                new { MissionId = 4, BestiaireId = 4 },
                new { MissionId = 4, BestiaireId = 5 }
            );
    }
}