using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Infrastructures.Entities;

namespace StarColonies.Infrastructures.Configuration;

public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(r => r.Description)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(r => r.Quantity)
            .IsRequired();
        
        builder.HasOne(r => r.TypeResource)
            .WithMany(tr => tr.Resources)
            .HasForeignKey(r => r.IdTypeResource)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(r => r.Colons)
            .WithMany(c => c.Resources)
            .UsingEntity(j => j.ToTable("ColonResource"));

        SeedResources(builder);
    }

    private void SeedResources(EntityTypeBuilder<Resource> builder)
    {
        builder.HasData();
    }
}