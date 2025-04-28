using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarColonies.Infrastructures.Entities;

namespace StarColonies.Infrastructures.Configuration;

public class ColonConfiguration : IEntityTypeConfiguration<Colon>
{
    public static string ID_COLON_1 = "ID_COLON_1";
    public static string ID_COLON_2 = "ID_COLON_2";
    public static string ID_COLON_3 = "ID_COLON_3";
    public static string ID_COLON_4 = "ID_COLON_4";
    
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
        
        builder.HasMany(c => c.ColonResources)
            .WithOne(cr => cr.Colon)
            .HasForeignKey(cr => cr.ColonId)
            .OnDelete(DeleteBehavior.Cascade);
        
        SeedColons(builder);
    }

    private void SeedColons(EntityTypeBuilder<Colon> builder)
{
    var hasher = new PasswordHasher<Colon>();

    var colon1 = new Colon()
    {
        Id = ID_COLON_1,
        UserName = "Alex Striker",
        NormalizedUserName = "ALEX STRIKER",
        Email = "alex.striker@example.com",
        NormalizedEmail = "ALEX.STRIKER@EXAMPLE.COM",
        EmailConfirmed = true,
        SecurityStamp = Guid.NewGuid().ToString(),
        ConcurrencyStamp = Guid.NewGuid().ToString(),
        DateBirth = DateTime.Now.AddYears(-20),
        Avatar = "avatar_1.png",
        IdProfession = 1,
        Endurance = 2,
        Strength = 5
    };
    colon1.PasswordHash = hasher.HashPassword(colon1, "Motdepasse123!");

    var colon2 = new Colon()
    {
        Id = ID_COLON_2,
        UserName = "Mira Nova",
        NormalizedUserName = "MIRA NOVA",
        Email = "mira.nova@example.com",
        NormalizedEmail = "MIRA.NOVA@EXAMPLE.COM",
        EmailConfirmed = true,
        SecurityStamp = Guid.NewGuid().ToString(),
        ConcurrencyStamp = Guid.NewGuid().ToString(),
        DateBirth = DateTime.Now.AddYears(-25),
        Avatar = "avatar_2.png",
        IdProfession = 2,
        Endurance = 2,
        Strength = 5
    };
    colon2.PasswordHash = hasher.HashPassword(colon2, "Motdepasse123!");

    var colon3 = new Colon()
    {
        Id = ID_COLON_3,
        UserName = "Elara Starfinder",
        NormalizedUserName = "ELARA STARFINDER",
        Email = "elara.starfinder@example.com",
        NormalizedEmail = "ELARA.STARFINDER@EXAMPLE.COM",
        EmailConfirmed = true,
        SecurityStamp = Guid.NewGuid().ToString(),
        ConcurrencyStamp = Guid.NewGuid().ToString(),
        DateBirth = DateTime.Now.AddYears(-30),
        Avatar = "avatar_3.png",
        IdProfession = 3,
        Endurance = 2,
        Strength = 5
    };
    colon3.PasswordHash = hasher.HashPassword(colon3, "Motdepasse123!");

    var colon4 = new Colon()
    {
        Id = ID_COLON_4,
        UserName = "Jason Blades",
        NormalizedUserName = "JASON BLADES",
        Email = "jason.blades@example.com",
        NormalizedEmail = "JASON.BLADES@EXAMPLE.COM",
        EmailConfirmed = true,
        SecurityStamp = Guid.NewGuid().ToString(),
        ConcurrencyStamp = Guid.NewGuid().ToString(),
        DateBirth = DateTime.Now.AddYears(-35),
        Avatar = "avatar_4.png",
        IdProfession = 4,
        Endurance = 2,
        Strength = 5
    };
    colon4.PasswordHash = hasher.HashPassword(colon4, "Motdepasse123!");
    
    var colon5 = new Colon()
    {
        Id = "ID_COLON_5",
        UserName = "Lyra Moonshadow",
        NormalizedUserName = "LYRA MOONSHADOW",
        Email = "lyra.moonshadow@example.com",
        NormalizedEmail = "LYRA.MOONSHADOW@EXAMPLE.COM",
        EmailConfirmed = true,
        SecurityStamp = Guid.NewGuid().ToString(),
        ConcurrencyStamp = Guid.NewGuid().ToString(),
        DateBirth = DateTime.Now.AddYears(-28),
        Avatar = "avatar_5.png",
        IdProfession = 5,
        Endurance = 3,
        Strength = 4
    };
    colon5.PasswordHash = hasher.HashPassword(colon5, "Motdepasse123!");

    var colon6 = new Colon()
    {
        Id = "ID_COLON_6",
        UserName = "Dax Ironfist",
        NormalizedUserName = "DAX IRONFIST",
        Email = "dax.ironfist@example.com",
        NormalizedEmail = "DAX.IRONFIST@EXAMPLE.COM",
        EmailConfirmed = true,
        SecurityStamp = Guid.NewGuid().ToString(),
        ConcurrencyStamp = Guid.NewGuid().ToString(),
        DateBirth = DateTime.Now.AddYears(-32),
        Avatar = "avatar_6.png",
        IdProfession = 6,
        Endurance = 5,
        Strength = 6
    };
    colon6.PasswordHash = hasher.HashPassword(colon6, "Motdepasse123!");

    var colon7 = new Colon()
    {
        Id = "ID_COLON_7",
        UserName = "Zara Flamewalker",
        NormalizedUserName = "ZARA FLAMEWALKER",
        Email = "zara.flamewalker@example.com",
        NormalizedEmail = "ZARA.FLAMEWALKER@EXAMPLE.COM",
        EmailConfirmed = true,
        SecurityStamp = Guid.NewGuid().ToString(),
        ConcurrencyStamp = Guid.NewGuid().ToString(),
        DateBirth = DateTime.Now.AddYears(-26),
        Avatar = "avatar_7.png",
        IdProfession = 7,
        Endurance = 4,
        Strength = 5
    };
    colon7.PasswordHash = hasher.HashPassword(colon7, "Motdepasse123!");

    var colon8 = new Colon()
    {
        Id = "ID_COLON_8",
        UserName = "Orion Starborn",
        NormalizedUserName = "ORION STARBORN",
        Email = "orion.starborn@example.com",
        NormalizedEmail = "ORION.STARBORN@EXAMPLE.COM",
        EmailConfirmed = true,
        SecurityStamp = Guid.NewGuid().ToString(),
        ConcurrencyStamp = Guid.NewGuid().ToString(),
        DateBirth = DateTime.Now.AddYears(-29),
        Avatar = "avatar_8.png",
        IdProfession = 8,
        Endurance = 3,
        Strength = 5
    };
    colon8.PasswordHash = hasher.HashPassword(colon8, "Motdepasse123!");

    var colon9 = new Colon()
    {
        Id = "ID_COLON_9",
        UserName = "Selene Frost",
        NormalizedUserName = "SELENE FROST",
        Email = "selene.frost@example.com",
        NormalizedEmail = "SELENE.FROST@EXAMPLE.COM",
        EmailConfirmed = true,
        SecurityStamp = Guid.NewGuid().ToString(),
        ConcurrencyStamp = Guid.NewGuid().ToString(),
        DateBirth = DateTime.Now.AddYears(-24),
        Avatar = "avatar_9.png",
        IdProfession = 9,
        Endurance = 4,
        Strength = 4
    };
    colon9.PasswordHash = hasher.HashPassword(colon9, "Motdepasse123!");

    var colon10 = new Colon()
    {
        Id = "ID_COLON_10",
        UserName = "Ryder Nightfall",
        NormalizedUserName = "RYDER NIGHTFALL",
        Email = "ryder.nightfall@example.com",
        NormalizedEmail = "RYDER.NIGHTFALL@EXAMPLE.COM",
        EmailConfirmed = true,
        SecurityStamp = Guid.NewGuid().ToString(),
        ConcurrencyStamp = Guid.NewGuid().ToString(),
        DateBirth = DateTime.Now.AddYears(-27),
        Avatar = "avatar_10.png",
        IdProfession = 10,
        Endurance = 5,
        Strength = 5
    };
    colon10.PasswordHash = hasher.HashPassword(colon10, "Motdepasse123!");


    builder.HasData(colon1, colon2, colon3, colon4, colon5, colon6, colon7, colon8, colon9, colon10);
}


}