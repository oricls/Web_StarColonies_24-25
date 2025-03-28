namespace StarColonies.Infrastructures.Entities;

public class Bonus
{
    public int IdBonus { get; set; }
    public string Nom { get; set; }
    public string Description { get; set; }
    public DateTime DateHeureValidite { get; set; }
    public DateTime DateHeureAchat { get; set; }
    
    // clés
    public List<Colon> Colons { get; set; }
}