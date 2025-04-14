using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Infrastructures.Entities;

namespace StarColonies.Infrastructures.Configuration;

public class AdminConfiguration : IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
        builder.HasKey(a => a.Id);
        
        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(a => a.Email)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(a => a.Password)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.HasOne<ActivityLog>(a => a.ActivityLog)
            .WithMany(al => al.Admins)
            .HasForeignKey(a => a.ActivityLogId)
            .OnDelete(DeleteBehavior.Cascade);
        
        SeedAdmins(builder);
    }

    private void SeedAdmins(EntityTypeBuilder<Admin> builder)
    {
        //builder.HasData();
    }
}