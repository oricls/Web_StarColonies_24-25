namespace StarColonies.Infrastructures.Entities;

public class ColonBonus
{
    public int Id { get; set; }
    public string ColonId { get; set; } = string.Empty;
    public Colon Colon { get; set; } = null!;
    
    public int BonusId { get; set; }
    public Bonus Bonus { get; set; } = null!;
    
    public DateTime DateAchat { get; set; }
    public DateTime DateExpiration { get; set; }
}