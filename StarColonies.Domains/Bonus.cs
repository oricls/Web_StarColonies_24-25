namespace StarColonies.Domains;

public class Bonus
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateAchat { get; set; }
    public DateTime DateExpiration { get; set; }
    public List<BonusResource> Resources { get; set; } = new List<BonusResource>();
}