namespace StarColonies.Domains;

public interface ITeamRepository
{
    void CreateTeam(Team team);
    void DeleteTeam(Team team);
    void UpdateTeamInfo(Team team);
    IReadOnlyList<Team> GetAllTeams();
    void AddMemberToTeam(Team team, Colon newMember);
    void RemoveMemberToTeam(Team team, Colon newMember);
    Team GetTeamById(int id);
    IList<Colon> GetMembersOfTeam(Team team);
    List<Team> GetTeamByColon(Colon colon);
}