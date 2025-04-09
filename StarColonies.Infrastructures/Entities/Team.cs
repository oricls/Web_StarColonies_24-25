namespace StarColonies.Infrastructures.Entities;

public class Team
{
    // clé primaire
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Logo { get; set; } = String.Empty;
    public string Baniere { get; set; } = String.Empty;
    
    // clés
    public Colon ColonCreator { get; set; } = null!;
    public int IdColonCreator { get; set; }

    public List<Colon> Members { get; set; } = new List<Colon>();
    
    public List<ResultatMission> ResultatMissions { get; set; } = new List<ResultatMission>();
}