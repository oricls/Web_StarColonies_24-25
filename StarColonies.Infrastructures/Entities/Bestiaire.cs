namespace StarColonies.Infrastructures.Entities;

public class Bestiaire
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public int Strength { get; set; }
    public int Endurance { get; set; }
    
    // clés étrangères
    public IList<Mission> Missions { get; set; } = null!;
    
    public TypeBestiaire TypeBestiaire { get; set; } = null!;
    public int IdTypeBestiaire { get; set; }
}