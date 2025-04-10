using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Infrastructures.Entities;

namespace StarColonies.Infrastructures.Configuration;

public class BestiaireConfiguration : IEntityTypeConfiguration<Bestiaire>
{
    public void Configure(EntityTypeBuilder<Bestiaire> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(128);
        
        builder.Property(c => c.Strength)
            .IsRequired();
        
        builder.Property(c => c.Endurance)
            .IsRequired();
        
        
        builder.HasOne(c => c.TypeBestiaire)
            .WithMany(c => c.Bestiaire)
            .HasForeignKey(c => c.IdTypeBestiaire)
            .OnDelete(DeleteBehavior.Restrict);
            
        SeedBestiaire(builder);
    }

    private void SeedBestiaire(EntityTypeBuilder<Bestiaire> builder)
    {
        var bestiaireList = new List<Bestiaire>
        {
            new()
            {
                Id = 1,
                Name = "Drone",
                Strength = 2,
                Endurance = 2,
                IdTypeBestiaire = 1
            },
            new()
            {
                Id = 2,
                Name = "Anomalie",
                Strength = 1,
                Endurance = 3,
                IdTypeBestiaire = 2
            },
            new()
            {
                Id = 3,
                Name = "Cryptoïde",
                Strength = 4,
                Endurance = 12,
                IdTypeBestiaire = 3
            },
            new()
            {
                Id = 4,
                Name = "Spectre",
                Strength = 7,
                Endurance = 1,
                IdTypeBestiaire = 4
            },
            new()
            {
                Id = 5,
                Name = "Nanobot",
                Strength = 1,
                Endurance = 2,
                IdTypeBestiaire = 1
            },
            new()
            {
                Id = 6,
                Name = "Prédateur",
                Strength = 4,
                Endurance = 3,
                IdTypeBestiaire = 5
            },
            new()
            {
                Id = 7,
                Name = "Chimère",
                Strength = 4,
                Endurance = 4,
                IdTypeBestiaire = 6
            },
            new()
            {
                Id = 8,
                Name = "Titan",
                Strength = 5,
                Endurance = 3,
                IdTypeBestiaire = 3
            },
            new()
            {
                Id = 9,
                Name = "Entité",
                Strength = 5,
                Endurance = 11,
                IdTypeBestiaire = 4
            },
            new()
            {
                Id = 10,
                Name = "Mutant",
                Strength = 2,
                Endurance = 4,
                IdTypeBestiaire = 7
            },
            new()
            {
                Id = 11,
                Name = "Leviathan",
                Strength = 9,
                Endurance = 7,
                IdTypeBestiaire = 3
            },
            new()
            {
                Id = 12,
                Name = "Hégémon",
                Strength = 20,
                Endurance = 20,
                IdTypeBestiaire = 3
            },
            new()
            {
                Id = 13,
                Name = "Drone de combat",
                Strength = 10,
                Endurance = 15,
                IdTypeBestiaire = 1
            }
        };

        builder.HasData(bestiaireList);
    }
}