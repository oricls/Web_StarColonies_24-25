namespace StarColonies.Domains;

public class ResultatMission
{
    public int Id { get; set; }
    public int Issue { get; set; }
    public string Date { get; set; } = string.Empty;
    public int MissionId { get; set; }
    public int TeamId { get; set; }
    public string TeamName { get; set; } = string.Empty;
    public string TeamLogo { get; set; } = string.Empty;
}