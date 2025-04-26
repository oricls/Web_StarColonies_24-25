namespace StarColonies.Domains;

public class TeamRankingModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Logo { get; set; }
    
    public string Baniere { get; set; }
    public int MissionsCompleted { get; set; }
    public int Score { get; set; }
    public double AverageLevel { get; set; }
}