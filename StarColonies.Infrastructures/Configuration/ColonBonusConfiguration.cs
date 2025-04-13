using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Infrastructures.Entities;

namespace StarColonies.Infrastructures.Configuration;

public class ColonBonusConfiguration : IEntityTypeConfiguration<ColonBonus>
{
    public void Configure(EntityTypeBuilder<ColonBonus> builder)
    {
        builder.HasKey(cb => cb.Id);
        
        builder.HasKey(cb => new { cb.ColonId, cb.BonusId });

        builder.Property(cb => cb.DateAchat)
            .IsRequired();

        builder.Property(cb => cb.DateExpiration)
            .IsRequired();

        builder.HasOne(cb => cb.Colon)
            .WithMany(c => c.ColonBonuses)
            .HasForeignKey(cb => cb.ColonId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(cb => cb.Bonus)
            .WithMany(b => b.ColonBonuses)
            .HasForeignKey(cb => cb.BonusId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}