using StarColonies.Domains;
using Microsoft.EntityFrameworkCore;

namespace StarColonies.Infrastructures;

public interface ITeamRepository
{
    void CreateTeam(Team team);
    void DeleteTeam(Team team);
    public void AddMemberToTeam(Team team, Colon newMember);
    public void RemoveMemberToTeam(Team team, Colon newMember);
    Team GetTeamById(int id);
    IList<Colon> GetMembersOfTeam(Team team);
}


public class TeamRepository : ITeamRepository
{
    private readonly StarColoniesContext _context;
    
    public TeamRepository(StarColoniesContext context)
    {
        _context = context;
    }

    public  void CreateTeam(Team team)
    {
        _context.Add(team);
        _context.SaveChanges();
    }
    
    public void DeleteTeam(Team team)
    { 
        _context.Remove(team);
        _context.SaveChanges();
    }

    public void AddMemberToTeam(Team team, Colon newMember)
    {
        if (team.MemberCount >= 5)
        {
            throw new Exception("La team est complète, impossible d'ajouter un nouveau membre");
        }
        
        var teamEntity = _context.Teams.SingleOrDefault(t => t.Id == team.Id);
        var memberEntity = _context.colon.SingleOrDefault(c => c.Id == newMember.Id.ToString()); // TODO id à revoir

        if (teamEntity.Members.Contains(memberEntity))
        {
            throw new Exception("Le joueur fait déja parti de la team");
        }

        teamEntity.Members.Add(memberEntity);
        _context.SaveChanges();
    }
    
    public void RemoveMemberToTeam(Team team, Colon newMember)
    {
        var teamEntity = _context.Teams.SingleOrDefault(t => t.Id == team.Id);
        var memberEntity = _context.colon.SingleOrDefault(c => c.Id == newMember.Id.ToString());
        
        if (!teamEntity.Members.Contains(memberEntity))
        {
            throw new Exception("Le joueur ne fait pas parti de la team");
        }

        teamEntity.Members.Remove(memberEntity);
        _context.SaveChanges();
    }

    public Team GetTeamById(int id)
    {
        var team = _context.Teams.SingleOrDefault(t => t.Id == id);
        
        
        if (team == null)
        {
            throw new KeyNotFoundException("Aucune equipe correspondante existente");
        }

        return MapTeamEntityToDomain(team);
    }

    public IList<Colon> GetMembersOfTeam(Team team)
    {
       var teamEntity = _context.Teams
                       .Include(t => t.Members)
                       .SingleOrDefault(t => t.Id == team.Id);

       return teamEntity.Members.Select(c => MapColonEntityToDomain(c)).ToList();
    }
    
    private Team MapTeamEntityToDomain(Entities.Team teamEntity)
    {
        var avg = teamEntity.Members.Average(m => m.Level);

        return new Team
        {
            Id = teamEntity.Id,
            Name = teamEntity.Name,
            Logo = teamEntity.Logo,
            MemberCount = teamEntity.Members.Count,
            AverageLevel = (int)avg,
            IsSelectedForMissions = false, // TODO a modifier 
        };
    }
    private Colon MapColonEntityToDomain(Entities.Colon colonEntity)
    {
        return new Colon
        {
            Id = int.Parse(colonEntity.Id), // id en TKey, faudra peut etre revoir l'id de domains.colon
            Name = colonEntity.NameColon,
            Email = colonEntity.Email,
            Password = colonEntity.PasswordHash
        };
    }
}