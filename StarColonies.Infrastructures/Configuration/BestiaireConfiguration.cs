using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Infrastructures.Entities;

namespace StarColonies.Infrastructures.Configuration;

public class BestiaireConfiguration : IEntityTypeConfiguration<Bestiaire>
{
    public void Configure(EntityTypeBuilder<Bestiaire> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(128);
        
        builder.Property(c => c.Strength)
            .IsRequired();
        
        builder.Property(c => c.Endurance)
            .IsRequired();
        
        builder.HasMany(c => c.Missions)
            .WithMany(c => c.Bestiaires)
            .UsingEntity(j => j.ToTable("BestiaireMission"));
        
        builder.HasOne(c => c.TypeBestiaire)
            .WithMany(c => c.Bestiaire)
            .HasForeignKey(c => c.IdTypeBestiaire)
            .OnDelete(DeleteBehavior.Cascade);
            
    }
}