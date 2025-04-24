using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Infrastructures.Entities;

namespace StarColonies.Infrastructures.Configuration;

public class BonusTransactionConfiguration : IEntityTypeConfiguration<BonusTransaction>
{
    public void Configure(EntityTypeBuilder<BonusTransaction> builder)
    {
        builder.HasKey(t => t.Id);
            
        builder.Property(t => t.TransactionDate)
            .IsRequired();
                
        builder.HasOne(t => t.Colon)
            .WithMany()
            .HasForeignKey(t => t.ColonId)
            .OnDelete(DeleteBehavior.Cascade);
                
        builder.HasOne(t => t.Bonus)
            .WithMany()
            .HasForeignKey(t => t.BonusId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}