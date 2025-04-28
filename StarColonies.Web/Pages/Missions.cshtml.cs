using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains;
using StarColonies.Domains.Repositories;
using StarColonies.Web.ViewModels;

namespace StarColonies.Web.Pages;

public class Missions : PageModel
{
    private readonly IMissionRepository _missionRepository;

    [BindProperty(SupportsGet = true)]
    public string SearchTerm { get; set; } = string.Empty;
    
    [BindProperty(SupportsGet = true)]
    public string DifficultyFilter { get; set; } = string.Empty;

    public List<MissionCardVm> AllMissions { get; set; } = [];

    public Missions(IMissionRepository missionRepository)
    {
        _missionRepository = missionRepository;
    }

    public async Task OnGetAsync()
    {
        var allMissions = await _missionRepository.GetAllMissionsAsync();
        
        // Filtrer les missions selon le terme de recherche si présent
        if (!string.IsNullOrWhiteSpace(SearchTerm))
        {
            allMissions = allMissions
                .Where(m => m.Name.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) || 
                            m.Description.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        
        // Filtrer par niveau de difficulté si sélectionné
        if (!string.IsNullOrWhiteSpace(DifficultyFilter))
        {
            allMissions = DifficultyFilter switch
            {
                "1-5" => allMissions.Where(m => m.Level is >= 1 and <= 5).ToList(),
                "6-10" => allMissions.Where(m => m.Level is >= 6 and <= 10).ToList(),
                "11-15" => allMissions.Where(m => m.Level is >= 11 and <= 15).ToList(),
                "16+" => allMissions.Where(m => m.Level >= 16).ToList(),
                _ => allMissions
            };
        }
        
        // Convertir les missions en ViewModel
        var missionsList = new List<MissionCardVm>();
        foreach (var mission in allMissions)
        {
            missionsList.Add(await ToMissionCardVm(mission));
        }
        
        AllMissions = missionsList;
    }

    private async Task<MissionCardVm> ToMissionCardVm(Mission mission)
    {
        var url = Url.Page("/ConsultMission", values: new { slug = mission.Name.ToKebab() });
    
        // Récupérer les bestiaires pour cette mission
        var bestiaires = await _missionRepository.GetBestiairesByMissionIdAsync(mission.Id);
    
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