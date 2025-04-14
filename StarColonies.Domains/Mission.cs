namespace StarColonies.Domains;

public class Mission
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Level { get; set; }
    
    // Propriétés calculées pour l'affichage
    public int BestiaireCount { get; set; }
    public int TotalEndurance { get; set; }
    public int TotalStrength { get; set; }
    
}