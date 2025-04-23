namespace StarColonies.Domains;

public class BonusResource
{
    public int ResourceId { get; set; }
    public string ResourceName { get; set; }
    public int Multiplier { get; set; }
    // AH J'AVAIS PAS CA HEIN
    public string IconUrl { get; set; }
    
    public string ResourceType { get; set; }
}