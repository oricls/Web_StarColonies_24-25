using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Infrastructures.Entities;

namespace StarColonies.Infrastructures.Configuration;

public class TypeBestiaireConfiguration : IEntityTypeConfiguration<TypeBestiaire>
{
    public void Configure(EntityTypeBuilder<TypeBestiaire> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(c => c.Avatar)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasMany(c => c.Bestiaire)
            .WithOne(c => c.TypeBestiaire)
            .HasForeignKey(c => c.IdTypeBestiaire)
            .OnDelete(DeleteBehavior.Restrict);

        SeedTypeBestiaire(builder);
    }

    private void SeedTypeBestiaire(EntityTypeBuilder<TypeBestiaire> builder)
    {
        var typeBestiaireList = new List<TypeBestiaire>
        {
            new()
            {
                Id = 1,
                Name = "Robot",
                Description =
                    "Créations mécaniques ou cybernétiques, souvent conçues pour des tâches spécifiques. Peuvent aller des assistants domestiques aux machines de guerre autonomes.",
                Avatar = "robot.png"
            },
            new()
            {
                Id = 2,
                Name = "Naturelle",
                Description =
                    "Êtres organiques issus de l'évolution naturelle, parfaitement adaptés à leur écosystème. Inclut les créatures des forêts, des océans et autres habitats terrestres.",
                Avatar = "naturelle.png"
            },
            new()
            {
                Id = 3,
                Name = "Extraterrestre",
                Description =
                    "Formes de vie originaires d'autres planètes ou dimensions, possédant souvent des caractéristiques biologiques exotiques et des capacités inexplicables.",
                Avatar = "extraterrestre.png"
            },
            new()
            {
                Id = 4,
                Name = "Paranormal",
                Description =
                    "Entités défiant les lois de la physique, souvent liées à des phénomènes spirituels ou énigmatiques. Inclut fantômes, esprits et créatures dimensionnelles.",
                Avatar = "paranormal.png"
            },
            new()
            {
                Id = 5,
                Name = "Animal",
                Description =
                    "Espèces animales terrestres, qu'elles soient communes ou rares. Peuvent inclure des variants évolués ou génétiquement modifiés.",
                Avatar = "animal.png"
            },
            new()
            {
                Id = 6,
                Name = "Expérience",
                Description =
                    "Résultats d'expérimentations scientifiques ou magiques, combinant souvent des traits de multiples espèces. Créatures instables aux capacités imprévisibles.",
                Avatar = "experience.png"
            },
            new()
            {
                Id = 7,
                Name = "Humanoïde",
                Description =
                    "Êtres bipèdes à morphologie semblable aux humains, qu'ils soient d'origine naturelle ou artificielle. Peuvent posséder une intelligence avancée et une société structurée.",
                Avatar = "humanoide.png"
            },
        };

        builder.HasData(typeBestiaireList);
    }
}