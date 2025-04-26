namespace StarColonies.Infrastructures.Entities;

public class Mission
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Image { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    
    // clés
    public IList<ResultatMission>? ResultatMissions { get; set; }
    
    public IList<MissionBestiaire> MissionBestiaires { get; set; } = new List<MissionBestiaire>();
    
    public IList<MissionResource> GainedResources { get; set; } = new List<MissionResource>();    
}