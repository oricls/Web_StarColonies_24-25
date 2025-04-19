namespace StarColonies.Infrastructures.Entities;

/**
 * Correspond à un bonus
 */
public class Bonus
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    public TimeSpan DureeParDefaut { get; set; } = TimeSpan.FromHours(24);
    
    // clés
    public IList<ColonBonus> ColonBonuses { get; set; } = new List<ColonBonus>();
    
    public IList<BonusResource> BonusResources { get; set; } = new List<BonusResource>();
}