namespace StarColonies.Domains.Repositories;

public interface IMissionRepository
{
    IReadOnlyList<Mission> GetAllMissions();
    Task<IReadOnlyList<Mission>> GetAllMissionsAsync();
    Mission GetMissionById(int missionId);
    Task<Mission> GetMissionByIdAsync(int missionId);
    IReadOnlyList<Bestiaire> GetBestiairesByMissionId(int missionId);
    Task<IReadOnlyList<Bestiaire>> GetBestiairesByMissionIdAsync(int missionId);
    IReadOnlyList<Mission> GetMissionsByTeamId(int teamId);
    Task<IReadOnlyList<Mission>> GetMissionsByTeamIdAsync(int teamId);
    IReadOnlyList<ResultatMission> GetResultatsByMissionId(int missionId);
    Task<IReadOnlyList<ResultatMission>> GetResultatsByMissionIdAsync(int missionId);
    void SaveOrUpdateMission(Mission mission);
    Task SaveOrUpdateMissionAsync(Mission mission);
    void SaveMissionResult(ResultatMission missionResult);
    Task<IReadOnlyList<Resource>> GetAllResources();
    Task<IReadOnlyList<Bestiaire>> GetAllBestiaires();
    Task AddMission(Mission mission);
}
