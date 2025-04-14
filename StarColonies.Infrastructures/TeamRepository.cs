using StarColonies.Domains;
using Microsoft.EntityFrameworkCore;

namespace StarColonies.Infrastructures;

public class TeamRepository : ITeamRepository
{
    private readonly StarColoniesContext _context;
    
    public TeamRepository(StarColoniesContext context)
    {
        _context = context;
    }

    public  void CreateTeam(Team team)
    {
        // faire validation de la création (envoi de message?)
        _context.Add(team);
        _context.SaveChanges();
    }
    public async Task CreateTeamAsync(Team team)
    {
        // faire validation de la création (envoi de message?)
        var TeamEntity = new Entities.Team
        {
            Name = team.Name,
            Logo = team.Logo,
            Baniere = team.Logo, // TODO a corriger si besoin
            IdColonCreator = "testUser" // pour tester un user de base en netUsers
        };
           
        await _context.Team.AddAsync(TeamEntity);
        await _context.SaveChangesAsync();
    }
    
    public void DeleteTeam(Team team)
    { 
        _context.Remove(team);
        _context.SaveChanges();
    }

    public async Task DeleteTeamAsync(Team team)
    {
        var TeamEntity = await _context.Team.FirstOrDefaultAsync(x => x.Id == team.Id);
        if (TeamEntity == null)
        {
            throw new NullReferenceException("Team inexistante");
        }

        _context.Team.Remove(TeamEntity);
        await _context.SaveChangesAsync();
    }

    public void UpdateTeamInfo(Team team)
    {
        var teamEntity = _context.Team.Include(t => t.Members).FirstOrDefault(t => t.Id == team.Id);
        
        teamEntity.Name = team.Name;
        teamEntity.Baniere = team.Logo; // bannière ?
        teamEntity.Logo = team.Logo;
        _context.SaveChanges();
    }

    public async Task<IReadOnlyList<Team> >GetAllTeams()
    {
        var teams = await _context.Team
                                    .Include(t => t.Members)
                                    .Include(rm => rm.ResultatMissions).ToListAsync();
        return teams.Select(t => MapTeamEntityToDomain(t)).ToList();
    }

    public void AddMemberToTeam(Team team, Colon newMember)
    {
        if (team.MemberCount >= 5)
        {
            throw new Exception("La team est complète, impossible d'ajouter un nouveau membre");
        }
        
        var teamEntity = _context.Team.SingleOrDefault(t => t.Id == team.Id);
        var memberEntity = _context.Colon.SingleOrDefault(c => c.Id == newMember.Id.ToString()); // TODO id à revoir

        if (teamEntity.Members.Contains(memberEntity))
        {
            throw new Exception("Le joueur fait déja parti de la team");
        }

        teamEntity.Members.Add(memberEntity);
        _context.SaveChanges();
    }
    
    public void RemoveMemberToTeam(Team team, Colon newMember)
    {
        var teamEntity = _context.Team.SingleOrDefault(t => t.Id == team.Id);
        var memberEntity = _context.Colon.SingleOrDefault(c => c.Id == newMember.Id.ToString());
        
        if (!teamEntity.Members.Contains(memberEntity))
        {
            throw new Exception("Le joueur ne fait pas parti de la team");
        }

        teamEntity.Members.Remove(memberEntity);
        _context.SaveChanges();
    }

    public async Task<Team> GetTeamById(int id)
    {
        var team = await _context.Team.Include(m => m.Members).SingleOrDefaultAsync(t => t.Id == id);
        
        if (team == null)
        {
            throw new KeyNotFoundException("Aucune equipe correspondante existente");
        }

        return MapTeamEntityToDomain(team);
    }

    public async Task<IReadOnlyList<Colon>> GetMembersOfTeam(Team team)
    {
        var teamEntity = await _context.Team
            .Include(t => t.Members)
            .SingleOrDefaultAsync(t => t.Id == team.Id);
        
        return teamEntity.Members.Select(c => MapColonEntityToDomain(c)).ToList();
    }

    public async Task< IReadOnlyList<Team>> GetTeamByColon(Colon colon)
    {
        var teamWithColon = await _context.Team
                                            .Include(t => t.Members)
                                            .Where(t => t.Members.Any(m => m.Id == colon.Id.ToString())).ToListAsync();
        return teamWithColon.Select(t => MapTeamEntityToDomain(t)).ToList();
    }

    private Team MapTeamEntityToDomain(Entities.Team teamEntity)
    {
        var avg = (teamEntity.Members.Count == 0) ? 0:  teamEntity.Members.Average(m => m.Level);

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