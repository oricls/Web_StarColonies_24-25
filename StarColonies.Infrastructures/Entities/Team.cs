namespace StarColonies.Infrastructures.Entities;

public class Team
{
    // clé primaire
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Logo { get; set; } = String.Empty;
    public string Baniere { get; set; } = String.Empty;
    
    // clés
    public Colon Colon { get; set; } = null!;
    public int IdColon { get; set; }

    public List<Colon> Colons { get; set; } = new List<Colon>();
    
    public List<ResultatMission> Missions { get; set; } = new List<ResultatMission>();
}