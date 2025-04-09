namespace StarColonies.Infrastructures.Entities;

public class ResultatMission
{
    public int Id { get; set; }
    public int Issue {get; set;}
    public string Date { get; set; } = String.Empty;
    
    // clés étrangères
    public int IdMission { get; set; }
    public Mission Mission { get; set; } = null!;
    
    public int IdTeam { get; set; }
    public Team Team { get; set; } = null!;
}