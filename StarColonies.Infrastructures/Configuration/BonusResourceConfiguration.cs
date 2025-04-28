using Microsoft.EntityFrameworkCore;
using StarColonies.Infrastructures.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StarColonies.Infrastructures.Configuration
{
    public class BonusResourceConfiguration : IEntityTypeConfiguration<BonusResource>
    {
        public void Configure(EntityTypeBuilder<BonusResource> builder)
        {
            // Clé primaire
            builder.HasKey(br => br.Id);
        
            // Index unique
            builder.HasIndex(br => new { br.BonusId, br.ResourceId });
        
            builder.Property(br => br.Quantite)
                .IsRequired();
        
            builder.HasOne(br => br.Bonus)
                .WithMany(b => b.BonusResources)
                .HasForeignKey(br => br.BonusId);
            
            builder.HasOne(br => br.Resource)
                .WithMany()
                .HasForeignKey(br => br.ResourceId);
                
            SeedBonusResources(builder);
        }
        
        private void SeedBonusResources(EntityTypeBuilder<BonusResource> builder)
        {
            var bonusResources = new List<BonusResource>
            {
                // Bonus 1: "Potion de force" - Utilise des ressources de différents types
                new BonusResource { Id = 1, BonusId = 1, ResourceId = 1, Quantite = 5 },  // Elindage léger (Type 1)
                new BonusResource { Id = 2, BonusId = 1, ResourceId = 11, Quantite = 3 }, // Sang fluorescent (Type 5)
                new BonusResource { Id = 3, BonusId = 1, ResourceId = 3, Quantite = 2 },  // Module quantique (Type 2)
                
                // Bonus 2: "Coup de pouce"
                new BonusResource { Id = 4, BonusId = 2, ResourceId = 6, Quantite = 1 },  // Mitraillette (Type 2)
                new BonusResource { Id = 5, BonusId = 2, ResourceId = 18, Quantite = 2 }, // Plan d'attaque (Type 4)
                
                // Bonus 3: "Potion d'endurance"
                new BonusResource { Id = 6, BonusId = 3, ResourceId = 4, Quantite = 4 },  // Nanofibres (Type 1)
                new BonusResource { Id = 7, BonusId = 3, ResourceId = 14, Quantite = 7 }, // Petite bière (Type 5)
                new BonusResource { Id = 8, BonusId = 3, ResourceId = 13, Quantite = 1 }, // Enorme casque (Type 3)
                
                // Bonus 4: "Grâce de Midas"
                new BonusResource { Id = 9, BonusId = 4, ResourceId = 12, Quantite = 3 },  // ADN cryptoïen (Type 4)
                new BonusResource { Id = 10, BonusId = 4, ResourceId = 17, Quantite = 1 }, // Cœur de l'Hégémon (Type 3)
                
                // Bonus 5: "Parchemin de monsieur Swinnen"
                new BonusResource { Id = 11, BonusId = 5, ResourceId = 20, Quantite = 5 }, // Clairevoyance (Type 4)
                new BonusResource { Id = 12, BonusId = 5, ResourceId = 2, Quantite = 3 },  // Batterie compacte (Type 2)
                new BonusResource { Id = 13, BonusId = 5, ResourceId = 15, Quantite = 8 }, // Ecaille solide (Type 1)
                
                // Bonus 6: "L'item trop cheatés"
                new BonusResource { Id = 14, BonusId = 6, ResourceId = 7, Quantite = 2 },  // Gros crâne d'Alex (Type 3)
                new BonusResource { Id = 15, BonusId = 6, ResourceId = 16, Quantite = 10 }, // Venin mortel (Type 5)
                new BonusResource { Id = 16, BonusId = 6, ResourceId = 19, Quantite = 4 }  // Membrane fantomique (Type 1)
            };
            
            builder.HasData(bonusResources);
        }
    }
}