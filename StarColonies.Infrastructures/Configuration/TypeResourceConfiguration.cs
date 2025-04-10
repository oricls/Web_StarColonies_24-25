using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Infrastructures.Entities;

namespace StarColonies.Infrastructures.Configuration;

public class TypeResourceConfiguration : IEntityTypeConfiguration<TypeResource>
{
    public void Configure(EntityTypeBuilder<TypeResource> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(c => c.Description)
            .IsRequired()
            .HasMaxLength(500);
        
        builder.Property(c => c.Icon)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.HasMany(c => c.Resources)   
            .WithOne(c => c.TypeResource)
            .HasForeignKey(c => c.IdTypeResource)
            .OnDelete(DeleteBehavior.Cascade);
        
        SeedTypeResources(builder);
        
    }

    private void SeedTypeResources(EntityTypeBuilder<TypeResource> builder)
    {
        builder.HasData();
    }
}