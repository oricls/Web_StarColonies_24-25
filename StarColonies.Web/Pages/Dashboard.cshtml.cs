using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains;
using StarColonies.Domains.Repositories;
using StarColonies.Web.ViewModels;
using Mission = StarColonies.Domains.Mission;
using Team = StarColonies.Domains.Team;

namespace StarColonies.Web.Pages;

public class Dashboard : PageModel
{
    private readonly IMissionRepository _missionRepository;
    private readonly ITeamRepository _teamRepository;
    private readonly IColonRepository _colonRepository;
    
    public IEnumerable<MissionCardVm> SuggestedMissions { get; set; } = new List<MissionCardVm>();
    public IReadOnlyList<Team> UserTeams { get; set; } = new List<Team>();
    public Dictionary<int, IReadOnlyList<Colon>> TeamMembers { get; set; } = new Dictionary<int, IReadOnlyList<Colon>>();

    public Dashboard(
        IMissionRepository missionRepository, 
        ITeamRepository teamRepository,
        IColonRepository colonRepository)
    {
        _missionRepository = missionRepository;
        _teamRepository = teamRepository;
        _colonRepository = colonRepository;
    }
    
    public async Task OnGetAsync()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (!string.IsNullOrEmpty(userId))
        {
            // Récupérer le colon associé à l'utilisateur connecté
            var colon = await _colonRepository.GetColonByIdAsync(userId);

            // Récupérer toutes les équipes dont l'utilisateur est membre
            UserTeams = await _teamRepository.GetTeamByColon(colon);
                
            // Pour chaque équipe, récupérer ses membres
            foreach (var team in UserTeams)
            {
                var members = await _teamRepository.GetMembersOfTeam(team);
                TeamMembers[team.Id] = members;
            }
        }
        else
        {
            // Ici on redirigera vers l'index je pense.
        }

        // Récupérer toutes les missions
        var allMissions = await _missionRepository.GetAllMissionsAsync();
        
        // Convertir les missions en ViewModel
        var missionsList = new List<MissionCardVm>();
        foreach (var mission in allMissions.Take(4))
        {
            missionsList.Add(await ToMissionCardVm(mission));
        }
        
        SuggestedMissions = missionsList;
    }
    
    private async Task<MissionCardVm> ToMissionCardVm(Mission mission)
    {
        var url = Url.Page("/ConsultMission", values: new { slug = mission.Name.ToKebab() });
    
        // Récupérer les bestiaires pour cette mission
        var bestiaires = await _missionRepository.GetBestiairesByMissionIdAsync(mission.Id);
    
        // Convertir les bestiaires en BestiaireIconVm 
        var bestiaireIcons = bestiaires
            //.Take(4) // Limiter à 4 icônes pour éviter d'encombrer l'interface
            .Select(b => new BestiaireIconVm(
                Name: b.Name,
                Avatar: b.TypeBestiaireAvatar, // Utiliser l'avatar stocké
                TypeName: b.TypeBestiaireName
            ))
            .ToList();
    
        return new MissionCardVm(
            Name: mission.Name,
            Description: mission.Description,
            Image: mission.Image,
            Level: mission.Level,
            BestiaireCount: mission.BestiaireCount,
            Target: url ?? string.Empty,
            BestiaireIcons: bestiaireIcons
        );
    }
}