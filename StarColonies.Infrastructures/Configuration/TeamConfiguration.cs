using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Infrastructures.Entities;

namespace StarColonies.Infrastructures.Configuration;

public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.HasKey(t => t.Id);
        
        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(t => t.Logo)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(t => t.Baniere)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.HasOne(t => t.ColonCreator)
            .WithMany()
            .HasForeignKey(t => t.IdColonCreator)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(t => t.Members)
            .WithMany(c => c.Teams)
            .UsingEntity(j => j.ToTable("TeamColon"));
        
        builder.HasMany(t => t.ResultatMissions)
            .WithOne(m => m.Team)
            .HasForeignKey(m => m.IdTeam)
            .OnDelete(DeleteBehavior.Cascade);
        
        SeedTeams(builder);
    }

    private void SeedTeams(EntityTypeBuilder<Team> builder)
    {
        //builder.HasData();
    }
}