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
        
        builder.HasOne(r => r.TypeResource)
            .WithMany(tr => tr.Resources)
            .HasForeignKey(r => r.IdTypeResource)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(r => r.ColonResources)
            .WithOne(cr => cr.Resource)
            .HasForeignKey(cr => cr.ResourceId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(r => r.MissionResources)
            .WithOne(mr => mr.Resource)
            .HasForeignKey(mr => mr.IdResource);

        SeedResources(builder);
    }

    private void SeedResources(EntityTypeBuilder<Resource> builder)
    {
        var resources = new List<Resource>()
        {
            // Ressources générales
            new Resource
            {
                Id = 1,
                Name = "Elindage léger",
                Description = "Matériau de protection léger",
                IdTypeResource = 1
            },
            new Resource
            {
                Id = 2,
                Name = "Batterie compacte",
                Description = "Technologie de stockage d'énergie",
                IdTypeResource = 2
            },
            new Resource
            {
                Id = 3,
                Name = "Module quantique",
                Description = "Composant technologique avancé",
                IdTypeResource = 2
            },
            new Resource
            {
                Id = 4,
                Name = "Nanofibres",
                Description = "Matériau haute résistance",
                IdTypeResource = 1
            },

            new Resource
            {
                Id = 5,
                Name = "Elindage renforcé",
                Description = "Matériau de protection renforcé",
                IdTypeResource = 1
            },
            new Resource
            {
                Id = 6,
                Name = "Mitraillette",
                Description = "Arme technologique",
                IdTypeResource = 2
            },
            new Resource
            {
                Id = 7,
                Name = "Gros crâne d'Alex",
                Description = "Artefact mystérieux",
                IdTypeResource = 3
            },
            new Resource
            {
                Id = 8,
                Name = "Residus louches",
                Description = "Matériau suspect",
                IdTypeResource = 1
            },

            new Resource
            {
                Id = 9,
                Name = "Vinade avariée",
                Description = "Substance consommable dégradée",
                IdTypeResource = 5
            },
            new Resource
            {
                Id = 10,
                Name = "Crocs tranchants",
                Description = "Matériau provenant de dents animales",
                IdTypeResource = 1
            },
            new Resource
            {
                Id = 11,
                Name = "Sang fluorescent",
                Description = "Substance consommable bioluminescente",
                IdTypeResource = 5
            },
            new Resource
            {
                Id = 12,
                Name = "ADN cryptoïen",
                Description = "Connaissance génétique",
                IdTypeResource = 4
            },

            new Resource
            {
                Id = 13,
                Name = "Enorme casque",
                Description = "Artefact de protection massive",
                IdTypeResource = 3
            },
            new Resource
            {
                Id = 14,
                Name = "Petite bière",
                Description = "Boisson consommable",
                IdTypeResource = 5
            },
            new Resource
            {
                Id = 15,
                Name = "Ecaille solide",
                Description = "Matériau de protection naturelle",
                IdTypeResource = 1
            },
            new Resource
            {
                Id = 16,
                Name = "Venin mortel",
                Description = "Substance consommable dangereuse",
                IdTypeResource = 5
            },

            new Resource
            {
                Id = 17,
                Name = "Cœur de l'Hégémon",
                Description = "Artefact énigmatique",
                IdTypeResource = 3
            },
            new Resource
            {
                Id = 18,
                Name = "Plan d'attaque",
                Description = "Connaissance stratégique",
                IdTypeResource = 4
            },
            new Resource
            {
                Id = 19,
                Name = "Membrane fantomique",
                Description = "Matériau spectral",
                IdTypeResource = 1
            },
            new Resource
            {
                Id = 20,
                Name = "Clairevoyance",
                Description = "Connaissance extrasensorielle",
                IdTypeResource = 4
            }
        };
        
        builder.HasData(resources);
    }
}