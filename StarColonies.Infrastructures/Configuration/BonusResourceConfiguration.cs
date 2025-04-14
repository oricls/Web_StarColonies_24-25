using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Infrastructures.Entities;

namespace StarColonies.Infrastructures.Configuration;

public class BonusResourceConfiguration : IEntityTypeConfiguration<BonusResource>
{
    public void Configure(EntityTypeBuilder<BonusResource> builder)
    {
        builder.HasKey(br => br.Id);
        
        builder.Property(br => br.Quantite)
            .IsRequired();
        
        builder.HasKey(br => new { br.BonusId, br.ResourceId });
        
        builder.HasOne(br => br.Bonus)
            .WithMany(b => b.BonusResources)
            .HasForeignKey(br => br.BonusId);
            
        builder.HasOne(br => br.Resource)
            .WithMany()
            .HasForeignKey(br => br.ResourceId);
    }
}