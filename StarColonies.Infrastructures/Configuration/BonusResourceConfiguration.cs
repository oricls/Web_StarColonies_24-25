using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Infrastructures.Entities;

namespace StarColonies.Infrastructures.Configuration;

public class BonusResourceConfiguration : IEntityTypeConfiguration<BonusResource>
{
    public void Configure(EntityTypeBuilder<BonusResource> builder)
    {
        // Choisir UNE SEULE définition de clé primaire
        builder.HasKey(br => br.Id);
    
        // Ne pas mettre HasKey ici une deuxième fois!
        // Plutôt utiliser un index unique
        builder.HasIndex(br => new { br.BonusId, br.ResourceId });
    
        builder.Property(br => br.Quantite)
            .IsRequired();
    
        builder.HasOne(br => br.Bonus)
            .WithMany(b => b.BonusResources)
            .HasForeignKey(br => br.BonusId);
        
        builder.HasOne(br => br.Resource)
            .WithMany()
            .HasForeignKey(br => br.ResourceId);
    }
}