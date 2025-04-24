using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Infrastructures.Entities;

namespace StarColonies.Infrastructures.Configuration;

public class LogConfiguration : IEntityTypeConfiguration<Log>
{
    public void Configure(EntityTypeBuilder<Log> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.RequeteAction)
            .IsRequired()
            .HasMaxLength(256);
        
        builder.Property(x => x.ResponseAction)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(x => x.DateHeureAction)
            .IsRequired();
    }
}