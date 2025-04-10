namespace StarColonies.Infrastructures.Entities;

public class MissionBestiaire
{
    public int Id { get; set; }
    
    public int IdMission { get; set; }
    public int IdBestiaire { get; set; }

    public Mission Mission { get; set; }
    public Bestiaire Bestiaire { get; set; }
}