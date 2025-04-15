using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StarColonies.Infrastructures.Configuration;
using StarColonies.Infrastructures.Entities;

namespace StarColonies.Infrastructures;

public class StarColoniesContext : IdentityDbContext<Colon> {
    public StarColoniesContext(DbContextOptions<StarColoniesContext> options) : base(options)
    {
    }
    
    public DbSet<Mission> Mission { get; set; }
    public DbSet<Bestiaire> Bestiaire { get; set; }
    public DbSet<TypeBestiaire> TypeBestiaire { get; set; }
    public DbSet<MissionBestiaire> MissionBestiaire { get; set; }
    public DbSet<ResultatMission> ResultatMission { get; set; }
    public DbSet<Team> Team { get; set; }
    
    public DbSet<Colon> Colon { get; set; } // ? ou AspNetUsers ?
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProfessionConfiguration());
        modelBuilder.ApplyConfiguration(new TypeBestiaireConfiguration());
        modelBuilder.ApplyConfiguration(new TypeResourceConfiguration());
    
        modelBuilder.ApplyConfiguration(new ResourceConfiguration());
        modelBuilder.ApplyConfiguration(new BestiaireConfiguration());
        modelBuilder.ApplyConfiguration(new BonusConfiguration());
    
        modelBuilder.ApplyConfiguration(new ColonConfiguration());
        modelBuilder.ApplyConfiguration(new TeamConfiguration());
        modelBuilder.ApplyConfiguration(new MissionConfiguration());
    
        modelBuilder.ApplyConfiguration(new ResultatMissionConfiguration());
        modelBuilder.ApplyConfiguration(new BonusResourceConfiguration());
        modelBuilder.ApplyConfiguration(new MissionBestiaireConfiguration());
        modelBuilder.ApplyConfiguration(new ColonBonusConfiguration());
        
        // Configurations pour le système d'administration
        modelBuilder.ApplyConfiguration(new ActivityLogConfiguration());
        modelBuilder.ApplyConfiguration(new LogConfiguration());
        modelBuilder.ApplyConfiguration(new AdminConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}