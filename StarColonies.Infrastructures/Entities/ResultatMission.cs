namespace StarColonies.Infrastructures.Entities;

public class ResultatMission
{
    public int Id { get; set; }
    public double IssueEndurance {get; set;}
    public double IssueStrength {get; set;}
    public DateTime Date { get; set; } = DateTime.Now;
    
    // clés étrangères
    public int IdMission { get; set; }
    public Mission Mission { get; set; } = null;
    
    public int IdTeam { get; set; }
    public Team Team { get; set; } = null;
}