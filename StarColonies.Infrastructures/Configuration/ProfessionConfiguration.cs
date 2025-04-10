using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Infrastructures.Entities;

namespace StarColonies.Infrastructures.Configuration;

public class ProfessionConfiguration : IEntityTypeConfiguration<Profession>
{
    public void Configure(EntityTypeBuilder<Profession> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(p => p.Icone)
            .IsRequired()
            .HasMaxLength(100);
        
        SeedProfessions(builder);
    }

    private void SeedProfessions(EntityTypeBuilder<Profession> builder)
    {
        var professions = new List<Profession>
        {
            new Profession
            {
                Id = 1,
                Name = "Ingénieur",
                Description = "Un ingénieur est une personne qui conçoit et construit des machines.",
                Icone = "avatars/engineer.png"
            },
            new Profession
            {
                Id = 2,
                Name = "Médecin",
                Description = "Un médecin est une personne qui soigne les maladies.",
                Icone = "avatars/doctor.png"
            },
            new Profession
            {
                Id = 3,
                Name = "Scientifique",
                Description = "Un scientifique est une personne qui étudie la science.",
                Icone = "avatars/scientist.png"
            },
            new Profession
            {
                Id = 4,
                Name = "Soldat",
                Description = "Un soldat est une personne qui combat pour son pays.",
                Icone = "avatars/soldier.png"
            },
        };
        builder.HasData(professions);
    }
}