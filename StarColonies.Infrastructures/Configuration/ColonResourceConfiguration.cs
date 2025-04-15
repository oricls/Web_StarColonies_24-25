using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Infrastructures.Entities;

namespace StarColonies.Infrastructures.Configuration;

public class ColonResourceConfiguration : IEntityTypeConfiguration<ColonResource>
{
    public void Configure(EntityTypeBuilder<ColonResource> builder)
    {
        // Configuration de la clé composite
        builder.HasKey(cr => new { cr.ColonId, cr.ResourceId });
        
        // Propriété Quantity avec valeur par défaut à 0
        builder.Property(cr => cr.Quantity)
            .IsRequired()
            .HasDefaultValue(0);
            
        // Ces configurations sont optionnelles car déjà définies dans Colon et Resource
        // Je les inclus pour plus de clarté
        builder.HasOne(cr => cr.Colon)
            .WithMany(c => c.ColonResources)
            .HasForeignKey(cr => cr.ColonId)
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.HasOne(cr => cr.Resource)
            .WithMany(r => r.ColonResources)
            .HasForeignKey(cr => cr.ResourceId)
            .OnDelete(DeleteBehavior.Cascade);
            
        // Configuration de la table
        builder.ToTable("ColonResource");
    }
    
    // private void SeedColonResources(EntityTypeBuilder<ColonResource> builder)
    // {
    //     builder.HasData(
    //         new ColonResource { ColonId = "id-colon-1", ResourceId = 1, Quantity = 5 },
    //         new ColonResource { ColonId = "id-colon-1", ResourceId = 2, Quantity = 10 }
    //     );
    // }
}