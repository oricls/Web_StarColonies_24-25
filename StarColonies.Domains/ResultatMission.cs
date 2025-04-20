namespace StarColonies.Domains;

public class ResultatMission
{
    public int Id { get; set; }
    public double IssueStrength { get; set; }
    public double IssueEndurance { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public int MissionId { get; set; }
    public int TeamId { get; set; }
    public string TeamName { get; set; } = string.Empty;
    public string TeamLogo { get; set; } = string.Empty;
    
    public bool IsSuccess { get; set; } = false;
}