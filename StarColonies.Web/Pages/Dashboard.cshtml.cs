using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains;
using StarColonies.Web.ViewModels;
using Mission = StarColonies.Domains.Mission;
using Team = StarColonies.Domains.Team;

namespace StarColonies.Web.Pages;

public class Dashboard : PageModel
{
    private readonly IMissionRepository _missionRepository;
    
    public IEnumerable<MissionCardVm> SuggestedMissions { get; set; } = new List<MissionCardVm>();
    public IReadOnlyList<Team> UserTeams { get; set; } // Vous aurez besoin d'un TeamRepository pour compléter ceci

    public Dashboard(IMissionRepository missionRepository)
    {
        _missionRepository = missionRepository;
    }
    
    public async Task OnGetAsync()
{
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