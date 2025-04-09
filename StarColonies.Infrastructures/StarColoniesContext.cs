using Microsoft.EntityFrameworkCore;
using StarColonies.Infrastructures.Configuration;

namespace StarColonies.Infrastructures;

public class StarColoniesContext  : DbContext
{
    public StarColoniesContext(DbContextOptions<StarColoniesContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
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
        
        // Configurations pour le système d'administration
        modelBuilder.ApplyConfiguration(new ActivityLogConfiguration());
        modelBuilder.ApplyConfiguration(new LogConfiguration());
        modelBuilder.ApplyConfiguration(new AdminConfiguration());
    }
}