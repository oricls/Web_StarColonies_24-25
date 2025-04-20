namespace StarColonies.Domains;

public class MissionEngine
{
    private readonly Random _random = new();

    public ResultatMission ExecuteMission(Mission mission, Team team)
    {
        // Validation des entrées
        ArgumentNullException.ThrowIfNull(mission);
        ArgumentNullException.ThrowIfNull(team);

        // Calcul des multiplicateurs aléatoires
        var teamMultiplier = GetRandomMultiplier(1.5, 2.5);
        var challengesMultiplier = GetRandomMultiplier(1.5, 2.5);

        // Calcul des forces
        var teamStrength = team.TotalStrength * teamMultiplier;
        var challengesStrength = mission.TotalStrength * challengesMultiplier;

        // Calcul des endurances
        var teamEndurance = team.TotalEndurance * teamMultiplier;
        var challengesEndurance = mission.TotalEndurance * challengesMultiplier;

        // Résultats
        var isStrengthSuccess = teamStrength > challengesStrength;
        var isEnduranceSuccess = teamEndurance > challengesEndurance;

        return new ResultatMission()
        {
            IsSuccess = isStrengthSuccess && isEnduranceSuccess,
            IssueStrength = teamStrength - challengesStrength,
            IssueEndurance = teamEndurance - challengesEndurance,
            Date = DateTime.UtcNow,
            MissionId = mission.Id,
            TeamId = team.Id,
        };
    }

    private double GetRandomMultiplier(double min, double max)
    {
        return _random.NextDouble() * (max - min) + min;
    }
}