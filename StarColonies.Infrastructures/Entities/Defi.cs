namespace StarColonies.Infrastructures.Entities;

public class Defi
{
    public int IdDefi { get; set; }
    public string Nom { get; set; }
    public int Force { get; set; }
    public int Endurence { get; set; }
    
    // clé
    public List<Mission> IdMissions { get; set; }
    public List<TypeBestiaire> IdBestiaires { get; set; }
}