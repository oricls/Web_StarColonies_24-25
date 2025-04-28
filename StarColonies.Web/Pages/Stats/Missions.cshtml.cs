using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains;
using StarColonies.Domains.Repositories;

namespace StarColonies.Web.Pages.Stats;

public class SingleMissionData
{
    public int Win { get; set; } = 0;
    public int Lose { get; set; } = 0;
    
    public double IssueEndurenceAvg { get; set; } = 0.0;
    public double IssueStrenghtAvg { get; set; } = 0.0;
    
}

public class MissionsModel(
    UserManager<Infrastructures.Entities.Colon> userManager,
    ITeamRepository teamRepository,
    IMissionRepository missionRepository,
    ILogger<ConsultStatsModel> logger)
    : PageModel
{

    public IReadOnlyList<Team> UserTeams { get; private set; } = new List<Team>();

    [BindProperty(SupportsGet = true)]
    [Display(Name = "Équipe")]
    public int SelectedTeamId { get; set; }
    
    public IReadOnlyList<Mission> HistoriqueMission { get; set; } = new List<Mission>();
    public Dictionary<int, List<Bestiaire>> DefisParMission { get; set; } = new();
    public Dictionary<int, SingleMissionData> MissionData { get; set; } = new();
    
    private async Task<Infrastructures.Entities.Colon> GetCurrentUserAsync()
    {
        var user = await userManager.GetUserAsync(User);
        if (user == null)
        {
            throw new ApplicationException("User impossible à obtenir");
        }
        return user;
    }
    
    public async Task<IActionResult> OnGetAsync()
    {
        var user = await GetCurrentUserAsync();
        UserTeams = await teamRepository.GetTeamByColon(user.Id);
        
        if (SelectedTeamId > 0)
        {
            HistoriqueMission = await missionRepository.GetMissionsByTeamIdAsync(SelectedTeamId);
            foreach (var mission in HistoriqueMission)
            {
                var defis = await missionRepository.GetBestiairesByMissionIdAsync(mission.Id);
                DefisParMission.Add(mission.Id, defis.ToList());
                
                var resultats = await missionRepository.GetResultatsByMissionIdAsync(mission.Id);
                
                MissionData[mission.Id] = new SingleMissionData
                {
                    IssueEndurenceAvg = resultats.Any() ? resultats.Average(r => r.IssueEndurance) : 0,
                    IssueStrenghtAvg = resultats.Any() ? resultats.Average(r => r.IssueStrength) : 0,
                    Win = resultats.Count(r => r.IsSuccess),
                    Lose = resultats.Count(r => !r.IsSuccess)
                };
            }
        }

        return Page();
    }
    
    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            var user = await GetCurrentUserAsync();
            UserTeams = await teamRepository.GetTeamByColon(user.Id);
            return Page();
        }

        return RedirectToPage(new { SelectedTeamId });
    }
}