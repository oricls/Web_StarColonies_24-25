namespace StarColonies.Infrastructures.Entities;

public class Team
{
    // clé primaire
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Logo { get; set; } = String.Empty;
    public string Baniere { get; set; } = String.Empty;
    
    // clés
    public Colon ColonCreator { get; set; }
    
    //D'un int à un string car géré par identity
    public string IdColonCreator { get; set; } = string.Empty;

    public IList<Colon> Members { get; set; } = new List<Colon>();
    
    public IList<ResultatMission> ResultatMissions { get; set; } = new List<ResultatMission>();
}