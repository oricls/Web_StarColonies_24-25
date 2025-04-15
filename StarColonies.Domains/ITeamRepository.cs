namespace StarColonies.Domains;

public interface ITeamRepository
{
    void CreateTeam(Team team);
    Task CreateTeamAsync(Team team);
    void DeleteTeam(Team team);
    Task DeleteTeamAsync(Team team);
    void UpdateTeamInfo(Team team);
    Task<IReadOnlyList<Team>> GetAllTeams();
    void AddMemberToTeam(Team team, Colon newMember);
    void RemoveMemberToTeam(Team team, Colon newMember);
    Task<Team> GetTeamById(int id);
    Task<IReadOnlyList<Colon>> GetMembersOfTeam(Team team);
    Task<IReadOnlyList<Team>> GetTeamByColon(Colon colon);
}