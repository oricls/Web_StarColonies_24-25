namespace StarColonies.Infrastructures.Entities;

/**
 * Correspond aux ressources àé changer pour obtenir un bonus en particulierr
 */
public class BonusResource
{
    public int Id { get; set; }
    public int BonusId { get; set; }
    public Bonus Bonus { get; set; } = null!;
    
    public int ResourceId { get; set; }
    public Resource Resource { get; set; } = null!;
    
    public int Quantite { get; set; }
}