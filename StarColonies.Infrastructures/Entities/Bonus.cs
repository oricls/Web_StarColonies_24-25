namespace StarColonies.Infrastructures.Entities;

public class Bonus
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime DateHeureAchat { get; set; }
    public DateTime DateHeureValidite { get; set; }
    
    // clés
    public Colon Colon { get; set; } = null!;
    public int IdColon { get; set; }
}