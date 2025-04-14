namespace StarColonies.Domains;

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Logo { get; set; }
    public int MemberCount { get; set; }
    public int AverageLevel { get; set; }
    public bool IsSelectedForMissions { get; set; }
}