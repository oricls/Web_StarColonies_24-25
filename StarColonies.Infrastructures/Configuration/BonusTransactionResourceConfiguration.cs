using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Infrastructures.Entities;

namespace StarColonies.Infrastructures.Configuration;

public class BonusTransactionResourceConfiguration : IEntityTypeConfiguration<BonusTransactionResource>
{
    public void Configure(EntityTypeBuilder<BonusTransactionResource> builder)
    {
        builder.HasKey(tr => tr.Id);
            
        builder.Property(tr => tr.Quantite)
            .IsRequired();
                
        builder.HasOne(tr => tr.Transaction)
            .WithMany(t => t.TransactionResources)
            .HasForeignKey(tr => tr.TransactionId)
            .OnDelete(DeleteBehavior.Cascade);
                
        builder.HasOne(tr => tr.BonusResource)
            .WithMany()
            .HasForeignKey(tr => tr.BonusResourceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}