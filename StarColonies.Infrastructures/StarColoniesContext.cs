using Microsoft.EntityFrameworkCore;

namespace StarColonies.Infrastructures;

public class StarColoniesContext  : DbContext
{
    public StarColoniesContext(DbContextOptions<StarColoniesContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}