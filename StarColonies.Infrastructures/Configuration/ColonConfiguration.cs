using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Infrastructures.Entities;

namespace StarColonies.Infrastructures.Configuration;

public class ColonConfiguration : IEntityTypeConfiguration<Colon>
{
    public void Configure(EntityTypeBuilder<Colon> builder)
    {
        //builder.HasKey(c => c.Id); -> cf. Colon entity
        
        // builder.Property(c => c.NameColon)
        //     .IsRequired()
        //     .HasMaxLength(100);
               
        builder.Property(c => c.DateBirth)
            .IsRequired()
            .HasMaxLength(20);
               
        builder.Property(c => c.Avatar)
            .HasMaxLength(255);
               
        builder.HasOne(c => c.Profession)
            .WithMany()
            .HasForeignKey(c => c.IdProfession)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(c => c.Teams)
            .WithMany(t => t.Members)
            .UsingEntity(j => j.ToTable("ColonTeam"));
    
        // Configuration one-to-many (en tant que créateur)
        builder.HasMany<Team>()
            .WithOne(t => t.ColonCreator)
            .HasForeignKey(t => t.IdColonCreator)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(cb => cb.ColonBonuses)
            .WithOne(b => b.Colon)
            .HasForeignKey(cb => cb.ColonId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(c => c.Resources)
            .WithMany(r => r.Colons)
            .UsingEntity(j => j.ToTable("ColonResource"));
        
        SeedColons(builder);
    }

    private void SeedColons(EntityTypeBuilder<Colon> builder)
    {
        //builder.HasData();  
    }
}