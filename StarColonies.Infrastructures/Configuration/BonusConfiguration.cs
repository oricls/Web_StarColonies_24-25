using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Infrastructures.Entities;

namespace StarColonies.Infrastructures.Configuration;

public class BonusConfiguration : IEntityTypeConfiguration<Bonus>
{
    public void Configure(EntityTypeBuilder<Bonus> builder)
    {
        builder.HasKey(b => b.Id);
        
        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(b => b.Description)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(b => b.DureeParDefaut)
            .IsRequired();
        
        builder.HasMany(b => b.ColonBonuses)
            .WithOne(cb => cb.Bonus)
            .HasForeignKey(cb => cb.BonusId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(b => b.BonusResources)
            .WithOne(br => br.Bonus)
            .HasForeignKey(br => br.BonusId)
            .OnDelete(DeleteBehavior.Cascade);
        
        SeedBonus(builder);
    }

    private void SeedBonus(EntityTypeBuilder<Bonus> builder)
    {
        var bonuses = new List<Bonus>
        {
            new Bonus
            {
                Id = 1,
                Name = "Potion de force",
                Description = "Augmente temporairement la force de tous les membres d'une équipe",
                DureeParDefaut = TimeSpan.FromMinutes(20)
            },
            new Bonus
            {
                Id = 2,
                Name = "Equipe de pouce",
                Description = "Investit un soldat supplémentaire pour les 3 prochaines missions",
                DureeParDefaut = TimeSpan.FromMinutes(3) // 3 missions comme durée
            },
            new Bonus
            {
                Id = 3,
                Name = "Potion d'endurance",
                Description = "Augmente temporairement l'endurance de tous les membres d'une équipe",
                DureeParDefaut = TimeSpan.FromMinutes(20)
            },
            new Bonus
            {
                Id = 4,
                Name = "Grâce de Midas",
                Description = "Double le nombre de ressources obtenues pour 1 mission",
                DureeParDefaut = TimeSpan.FromMinutes(10) // 1 mission comme durée
            },
            new Bonus
            {
                Id = 5,
                Name = "Seconde chance",
                Description = "Chaque colon se voit octroyer une vie supplémentaire (endurance ×2)",
                DureeParDefaut = TimeSpan.FromMinutes(10) // 1 mission comme durée
            },
            new Bonus
            {
                Id = 6,
                Name = "Litem trop cheats",
                Description = "Elimine instantanément tous les ministres",
                DureeParDefaut = TimeSpan.FromMinutes(100)
            }
        };

        builder.HasData(bonuses);
    }
}