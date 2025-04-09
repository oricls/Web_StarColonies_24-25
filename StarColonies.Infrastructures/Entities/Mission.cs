namespace StarColonies.Infrastructures.Entities;

public class Mission
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public int DifficutyLevel { get; set; }
    public string Image { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    
    // clés
    public List<Team> Equipe { get; set; } = new List<Team>();
    
    public ResultatMission Resultat { get; set; } = null!;
    public int IdResultat { get; set; }
    
    public List<Bestiaire> Bestiaire { get; set; } = new List<Bestiaire>();
}