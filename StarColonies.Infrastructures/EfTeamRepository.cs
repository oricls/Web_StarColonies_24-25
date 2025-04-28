using StarColonies.Domains;
using Microsoft.EntityFrameworkCore;
using StarColonies.Domains.Repositories;

namespace StarColonies.Infrastructures;

public class EfTeamRepository : ITeamRepository
{
    private readonly StarColoniesContext _context;
    
    public EfTeamRepository(StarColoniesContext context)
    {
        _context = context;
    }
    
    public async Task CreateTeamAsync(Team team)
    {
        // Correction pour utiliser le CreatorId fourni au lieu de la valeur codée en dur
        var teamEntity = new Entities.Team
        {
            Name = team.Name,
            Logo = team.Logo,
            Baniere = team.Baniere, // Utiliser la valeur bannière correcte
            IdColonCreator = team.CreatorId // Utiliser l'ID du créateur fourni
        };
       
        await _context.Team.AddAsync(teamEntity);
        await _context.SaveChangesAsync();
    
        // Récupérer l'id
        team.Id = teamEntity.Id;
    }

    public async Task DeleteTeamAsync(Team team)
    {
        var teamEntity = await _context.Team.FirstOrDefaultAsync(t => t.Id == team.Id);
        if (teamEntity == null)
        {
            throw new NullReferenceException("Team inexistante");
        }

        _context.Team.Remove(teamEntity);
        await _context.SaveChangesAsync();
    }

    public void UpdateTeamInfo(Team team)
    {
        var teamEntity = _context.Team.Include(t => t.Members).FirstOrDefault(t => t.Id == team.Id);
        
        teamEntity.Name = team.Name;
        teamEntity.Baniere = team.Baniere; // bannière ?
        teamEntity.Logo = team.Logo;
        _context.SaveChanges();
    }

    public async Task<IReadOnlyList<Team> >GetAllTeams()
    {
        var teams = await _context.Team
                                    .Include(t => t.Members)
                                    //.Include(rm => rm.ResultatMissions)
                                    .ToListAsync();
        return teams.Select(t => MapTeamEntityToDomain(t)).ToList();
    }

    public void AddMemberToTeam(Team team, Colon newMember)
    {
        if (team.MemberCount >= 5)
        {
            throw new Exception("La team est complète, impossible d'ajouter un nouveau membre");
        }
        
        var teamEntity = _context.Team.SingleOrDefault(t => t.Id == team.Id);
        var memberEntity = _context.Colon.SingleOrDefault(c => c.Id == newMember.Id); // TODO id à revoir

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
        var memberEntity = _context.Colon.SingleOrDefault(c => c.Id == newMember.Id);
        
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

    public async Task< IReadOnlyList<Team>> GetTeamByColon(string colonId)
    {
        var teamWithColon = await _context.Team
                                            .Include(t => t.Members)
                                            .Where(t => t.Members.Any(m => m.Id == colonId)).ToListAsync();
        return teamWithColon.Select(t => MapTeamEntityToDomain(t)).ToList();
    }

    // Ici on utilise l'id directement
    public async Task AddMemberToTeamAsync(int teamId, string colonId)
    {
        // Vérifier si l'équipe existe
        var teamEntity = await _context.Team
            .Include(t => t.Members)
            .SingleOrDefaultAsync(t => t.Id == teamId);
            
        if (teamEntity == null)
            throw new KeyNotFoundException($"Équipe avec ID {teamId} non trouvée");
            
        // Vérifier si le colon existe
        var colon = await _context.Users
            .SingleOrDefaultAsync(c => c.Id == colonId);
            
        if (colon == null)
            throw new KeyNotFoundException($"Colon avec ID {colonId} non trouvé");
            
        // Vérifier si le colon est déjà membre de l'équipe
        if (teamEntity.Members.Any(c => c.Id == colonId))
            return; // Le colon est déjà membre de l'équipe
            
        // Ajouter le colon à l'équipe
        teamEntity.Members.Add(colon);
        await _context.SaveChangesAsync();
    }

    public void LevelUpTeam(Team team)
    {
        var teamEntity = _context.Team.Include(t => t.Members).FirstOrDefault(t => t.Id == team.Id);
        
        if (teamEntity == null)
        {
            throw new NullReferenceException("Team inexistante");
        }

        foreach (var member in teamEntity.Members)
        {
            member.Level++;
        }
        
        _context.SaveChanges();
    }
    
    public async Task<IReadOnlyList<Team>> GetTeamsWithoutMissionParticipationAsync(string userId, int missionId)
    {
        var userTeams = await _context.Team
            .Where(t => t.Members.Any(m => m.Id == userId)).Include(team => team.Members)
            .ToListAsync();

        var teamsWithParticipation = await _context.ResultatMission
            .Where(rm => rm.IdMission == missionId && rm.IssueStrength > 0 && rm.IssueEndurance > 0)
            .Select(rm => rm.IdTeam)
            .Distinct()
            .ToListAsync();

        var teamsWithoutParticipation = userTeams
            .Where(t => !teamsWithParticipation.Contains(t.Id))
            .ToList();

        return teamsWithoutParticipation.Select(MapTeamEntityToDomain).ToList();
    }

    public async Task<bool> IsUserMemberOfTeam(string userId, int teamId)
    {
        var team = await GetTeamById(teamId);

        var members = await GetMembersOfTeam(team);
        return members.Any(m => m.Id == userId);
    }


    public async Task<IReadOnlyList<TeamRankingModel>> GetTopTeamsAsync(int count = 10)
    {
        var teams = await _context.Team
            .Include(t => t.Members)
            .Include(t => t.ResultatMissions)
            .ToListAsync();
    
        var teamRankings = teams.Select(team => 
            {
                // Une mission est réussie si issueStrength > 0 ET issueEndurance > 0
                var missionsCompleted = team.ResultatMissions?
                    .Count(rm => rm.IssueStrength > 0 && rm.IssueEndurance > 0) ?? 0;
        
                int score = CalculateTeamScore(team);
        
                return new TeamRankingModel
                {
                    Id = team.Id,
                    Name = team.Name,
                    Logo = team.Logo,
                    Baniere = team.Baniere,
                    MissionsCompleted = missionsCompleted,
                    Score = score,
                    AverageLevel = team.Members.Count > 0 ? (int)Math.Round(team.Members.Average(m => m.Level)) : 0
                };
            })
            .OrderByDescending(t => t.Score)
            .ThenByDescending(t => t.MissionsCompleted)
            .ThenByDescending(t => t.AverageLevel)
            .Take(count)
            .ToList();
    
        return teamRankings;
    }

    public async Task<int> GetSuccessfulMissionsCountAsync(int teamId)
    {
        var teamEntity = await _context.Team
            .Include(t => t.ResultatMissions)
            .FirstOrDefaultAsync(t => t.Id == teamId);
        
        if (teamEntity == null)
            return 0;
        
        // TODO : Modularité avec top teams
        // Une mission est réussie si issueStrength > 0 ET issueEndurance > 0
        return teamEntity.ResultatMissions?
            .Count(rm => rm.IssueStrength > 0 && rm.IssueEndurance > 0) ?? 0;
    }

    private int CalculateTeamScore(Entities.Team team)
    {
        if (!team.Members.Any())
            return 0;
        
        // Score de base basé sur le niveau des membres
        int baseScore = team.Members.Sum(m => m.Level) * 100;
    
        // Bonus pour les missions réussies
        int missionBonus = 0;
        if (team.ResultatMissions.Any())
        {
            missionBonus = team.ResultatMissions
                .Where(rm => rm.IssueStrength > 0 && rm.IssueEndurance > 0)  // Mission réussie
                .Sum(rm => rm.IdMission * 10);
        }
    
        // Bonus pour la diversité des professions
        var uniqueProfessions = team.Members
            .Select(m => m.IdProfession)
            .Distinct()
            .Count();
        int diversityBonus = uniqueProfessions * 50;
    
        return baseScore + missionBonus + diversityBonus;
    }

    private Team MapTeamEntityToDomain(Entities.Team teamEntity)
    {
        var avg = (teamEntity.Members.Count == 0) ? 0:  teamEntity.Members.Average(m => m.Level);

        return new Team
        {
            Id = teamEntity.Id,
            Name = teamEntity.Name,
            Logo = teamEntity.Logo,
            Baniere = teamEntity.Baniere,
            MemberCount = teamEntity.Members.Count,
            AverageLevel = (int)Math.Round(avg),
            IsSelectedForMissions = false, // TODO : a modifier, je dirais même mieux : à implémenter 
            TotalStrength = teamEntity.Members.Sum(m => m.Strength + m.Level),
            TotalEndurance = teamEntity.Members.Sum(m => m.Endurance + m.Level),
            CreatorId = teamEntity.IdColonCreator
        };
    }
    private Colon MapColonEntityToDomain(Entities.Colon colonEntity)
    {
        return new Colon
        {
            Id = colonEntity.Id, // id en TKey, faudra peut etre revoir l'id de domains.colon -> oui j'ai revu
            Name = colonEntity.UserName ?? "Inconnu",
            Email = colonEntity.Email ?? "Inconnue",
            Password = colonEntity.PasswordHash ?? "",
            Avatar = colonEntity.Avatar,
            Level = colonEntity.Level,
            Strength = colonEntity.Strength,
            Endurance = colonEntity.Endurance,
            ProfessionName = GetProfessionName(colonEntity.IdProfession)
        };
    }
    
    private string GetProfessionName(int? professionId)
    {
        if (!professionId.HasValue)
        {
            return "Inconnue";
        }

        // Utiliser une méthode pour récupérer le nom de la profession à partir de l'ID
        var profession = _context.Profession.FirstOrDefault(p => p.Id == professionId.Value);
        return profession?.Name ?? "Inconnue";
    }
}