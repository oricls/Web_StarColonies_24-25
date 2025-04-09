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
        
        builder.Property(b => b.DateHeureAchat)
            .IsRequired();
        
        builder.Property(b => b.DateHeureValidite)
            .IsRequired();
        
        builder.HasOne(b => b.Colon)
            .WithMany(c => c.Bonuses)
            .HasForeignKey(b => b.IdColon)
            .OnDelete(DeleteBehavior.Cascade);
        
        SeedBonus(builder);
    }

    private void SeedBonus(EntityTypeBuilder<Bonus> builder)
    {
        builder.HasData();
    }
}