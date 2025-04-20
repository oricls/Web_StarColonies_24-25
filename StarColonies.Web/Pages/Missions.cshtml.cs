using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains;
using StarColonies.Web.ViewModels;

namespace StarColonies.Web.Pages;

public class Missions(IMissionRepository missionRepository) : PageModel
{
    public List<MissionCardVm> SuggestedMissions { get; set; } = [];

    public async Task OnGetAsync()
    {
        var allMissions = await missionRepository.GetAllMissionsAsync();
        
        // Convertir les missions en ViewModel
        var missionsList = new List<MissionCardVm>();
        foreach (var mission in allMissions)
        {
            missionsList.Add(await ToMissionCardVm(mission));
        }
        
        SuggestedMissions = missionsList;
    }
    private async Task<MissionCardVm> ToMissionCardVm(Mission mission)
    {
        var url = Url.Page("/ConsultMission", values: new { slug = mission.Name.ToKebab() });
    
        // Récupérer les bestiaires pour cette mission
        var bestiaires = await missionRepository.GetBestiairesByMissionIdAsync(mission.Id);
    
        // Convertir les bestiaires en BestiaireIconVm 
        var bestiaireIcons = bestiaires
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