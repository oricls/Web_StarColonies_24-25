using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Infrastructures.Entities;

namespace StarColonies.Infrastructures.Configuration;

public class MissionConfiguration : IEntityTypeConfiguration<Mission>
{
    public void Configure(EntityTypeBuilder<Mission> builder)
    {
        builder.HasKey(m => m.Id);
        
        builder.Property(m => m.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(m => m.DifficutyLevel)
            .IsRequired();
        
        builder.Property(m => m.Image)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(m => m.Description)
            .IsRequired()
            .HasMaxLength(500);
        
        builder.HasMany(m => m.ResultatMissions)
            .WithOne()
            .HasForeignKey(rm => rm.IdMission)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(m => m.Bestiaires)
            .WithMany(b => b.Missions)
            .UsingEntity(j => j.ToTable("MissionBestiaire"));
        
        SeedMissions(builder);
    }

    private void SeedMissions(EntityTypeBuilder<Mission> builder)
    {
        builder.HasData();
    }
}