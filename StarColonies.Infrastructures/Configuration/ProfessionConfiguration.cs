using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Infrastructures.Entities;

namespace StarColonies.Infrastructures.Configuration;

public class ProfessionConfiguration : IEntityTypeConfiguration<Profession>
{
    public void Configure(EntityTypeBuilder<Profession> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(p => p.Icone)
            .IsRequired()
            .HasMaxLength(100);
        
        SeedProfessions(builder);
    }

    private void SeedProfessions(EntityTypeBuilder<Profession> builder)
    {
        builder.HasData();
    }
}