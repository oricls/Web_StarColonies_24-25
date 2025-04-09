using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Infrastructures.Entities;

namespace StarColonies.Infrastructures.Configuration;

public class TypeBestiaireConfiguration : IEntityTypeConfiguration<TypeBestiaire>
{
    public void Configure(EntityTypeBuilder<TypeBestiaire> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(c => c.Description)
            .IsRequired()
            .HasMaxLength(500);
        
        builder.Property(c => c.Avatar)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.HasMany(c => c.Bestiaire)
            .WithOne(c => c.TypeBestiaire)
            .HasForeignKey(c => c.IdTypeBestiaire)
            .OnDelete(DeleteBehavior.Cascade);
        
        SeedTypeBestiaire(builder);
    }

    private void SeedTypeBestiaire(EntityTypeBuilder<TypeBestiaire> builder)
    {
        builder.HasData();
    }
}