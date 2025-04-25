namespace StarColonies.Domains.Repositories;

public interface ITeamRepository
{
    //void CreateTeam(Team team); -> utiliser async
    Task CreateTeamAsync(Team team);
    //void DeleteTeam(Team team); -> utiliser async
    Task DeleteTeamAsync(Team team);
    void UpdateTeamInfo(Team team);
    Task<IReadOnlyList<Team>> GetAllTeams();
    void AddMemberToTeam(Team team, Colon newMember);
    void RemoveMemberToTeam(Team team, Colon newMember);
    Task<Team> GetTeamById(int id);
    Task<IReadOnlyList<Colon>> GetMembersOfTeam(Team team);
    Task<IReadOnlyList<Team>> GetTeamByColon(string colonId);
    Task AddMemberToTeamAsync(int teamId, string userId);
    void LevelUpTeam(Team team);
}