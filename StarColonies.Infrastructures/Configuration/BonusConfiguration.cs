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
            
        builder.Property(b => b.IconUrl)
            .HasMaxLength(255);
            
        builder.Property(b => b.EffectTypeId)
            .IsRequired()
            .HasDefaultValue(0);
        
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
                DureeParDefaut = TimeSpan.FromMinutes(5),
                EffectTypeId = 1, // DoubleStrength
                IconUrl = "big_potion.png"
            },
            new Bonus
            {
                Id = 2,
                Name = "Coup de pouce",
                Description = "Investit un soldat supplémentaire pour les prochaines missions",
                DureeParDefaut = TimeSpan.FromMinutes(3), // 3 missions comme durée
                EffectTypeId = 3, // IncreaseLevel
                IconUrl = "soldat.png"
            },
            new Bonus
            {
                Id = 3,
                Name = "Potion d'endurance",
                Description = "Augmente temporairement l'endurance de tous les membres d'une équipe",
                DureeParDefaut = TimeSpan.FromMinutes(5),
                EffectTypeId = 2, // DoubleEndurance
                IconUrl = "big_potion.png"
            },
            new Bonus
            {
                Id = 4,
                Name = "Grâce de Midas",
                Description = "Double le nombre de ressources obtenues",
                DureeParDefaut = TimeSpan.FromMinutes(1), 
                EffectTypeId = 4, // DoubleResources
                IconUrl = "midas.png"
            },
            new Bonus
            {
                Id = 5,
                Name = "Parchemin de monsieur Swinnen",
                Description = "Augmente l'expérience gagnée et permet de gagner un niveau supplémentaire à chaque mission",
                DureeParDefaut = TimeSpan.FromMinutes(20),
                EffectTypeId = 5, // ExperienceBoost
                IconUrl = "life.png"
            },
            new Bonus
            {
                Id = 6,
                Name = "L'item trop cheatés",
                Description = "Elimine instantanément tous les monstres (ne fonctionne pas)",
                DureeParDefaut = TimeSpan.FromMinutes(100),
                EffectTypeId = 0, //TODO: HAHA C'EST COMPLIQUE CELUI CI
                IconUrl = "cheat.png"
            }
        };

        builder.HasData(bonuses);
    }
}