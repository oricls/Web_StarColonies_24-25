namespace StarColonies.Infrastructures.Entities;

public class Equipe
{
    // clé primaire
    public int IdEquipe { get; set; }
    
    public string nom { get; set; }
    public string logo { get; set; }
    
    // clés
    public List<Mission> IdMissions { get; set; }
}