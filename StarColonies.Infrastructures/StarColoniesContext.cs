using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StarColonies.Infrastructures.Configuration;
using StarColonies.Infrastructures.Entities;
using Transaction = System.Transactions.Transaction;

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
    public DbSet<ColonResource> ColonResource { get; set; }
    public DbSet<ColonBonus> ColonBonus { get; set; }
    public DbSet<Bonus> Bonus { get; set; }
    public DbSet<Resource> Resource { get; set; }
    public DbSet<Profession> Profession { get; set; }
    public DbSet<TypeResource> TypeResource { get; set; }
    public DbSet<BonusResource> BonusResource { get; set; }
    public DbSet<Log> Logs { get; set; }
    public DbSet<BonusTransaction> BonusTransaction { get; set; }
    public DbSet<BonusTransactionResource> BonusTransactionResource { get; set; }
    public DbSet<MissionResource> MissionResource { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 1. Configurations de base et types
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new ProfessionConfiguration());
        modelBuilder.ApplyConfiguration(new TypeBestiaireConfiguration());
        modelBuilder.ApplyConfiguration(new TypeResourceConfiguration());
    
        // 2. Entités principales autonomes
        modelBuilder.ApplyConfiguration(new ResourceConfiguration());
        modelBuilder.ApplyConfiguration(new BestiaireConfiguration());
        modelBuilder.ApplyConfiguration(new BonusConfiguration());
        modelBuilder.ApplyConfiguration(new ColonConfiguration());
        modelBuilder.ApplyConfiguration(new TeamConfiguration());
        modelBuilder.ApplyConfiguration(new MissionConfiguration());
    
        // 3. Tables de jointure (relations many-to-many)
        modelBuilder.ApplyConfiguration(new MissionBestiaireConfiguration());
        modelBuilder.ApplyConfiguration(new ColonResourceConfiguration());
        modelBuilder.ApplyConfiguration(new ColonBonusConfiguration());
        modelBuilder.ApplyConfiguration(new BonusResourceConfiguration());
        modelBuilder.ApplyConfiguration(new MissionResourceConfiguration());
    
        // 4. Entités liées aux opérations
        modelBuilder.ApplyConfiguration(new ResultatMissionConfiguration());
        modelBuilder.ApplyConfiguration(new BonusTransactionConfiguration());
        modelBuilder.ApplyConfiguration(new BonusTransactionResourceConfiguration());
    
        // 5. Configurations administratives
        modelBuilder.ApplyConfiguration(new LogConfiguration());
    
        base.OnModelCreating(modelBuilder);
    }
}