using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains;
using StarColonies.Domains.Repositories;

namespace StarColonies.Web.Pages;

public class StatPoint
{
    public string Date { get; set; }
    public double Endurance { get; set; }
    public double Strength { get; set; }
}

public class VictoryData
{
    public int TotalSuccess { get; private set; } = 0;
    public int TotalFailure { get; private set; } = 0;
    
    public void AddPoint(int success, int failure)
    {
        TotalSuccess += success;
        TotalFailure += failure;
    }
}

public class ConsultStatsModel(UserManager<Infrastructures.Entities.Colon> userManager, ITeamRepository teamRepository,
    IMissionRepository missionRepository, ILogger<ConsultStatsModel> logger)
    : PageModel
{
    public IReadOnlyList<Team> UserTeams { get; private set; } = new List<Team>();

    [BindProperty(SupportsGet = true)]
    [Display(Name = "Équipe")]
    public int SelectedTeamId { get; set; }
    

    public List<StatPoint> StatsEvolution { get; private set; } = new();
    public VictoryData StatVictoryData { get; private set; } = new();

    private async Task<Infrastructures.Entities.Colon> GetCurrentUserAsync()
    {
        var user = await userManager.GetUserAsync(User);
        if (user == null)
        {
            throw new ApplicationException("User impossible à obtenir");
        }
        return user;
    }

    public async Task<IActionResult> OnGet()
    {
        var user = await GetCurrentUserAsync();
        UserTeams = await teamRepository.GetTeamByColon(user.Id);

        if (SelectedTeamId > 0)
        {
            var missionTeam = await missionRepository.GetMissionsByTeamIdAsync(SelectedTeamId);

            foreach (var mission in missionTeam)
            {
                var resultMission = await missionRepository.GetResultatsByMissionIdAsync(mission.Id);
                foreach (var result in resultMission)
                {
                    StatsEvolution.Add(new StatPoint
                    {
                        Date = result.Date.ToString("dd-MM-yyyy"),
                        Strength = result.IssueStrength,
                        Endurance = result.IssueEndurance
                    });
                }

                int successes = resultMission.Count(m => m.IsSuccess);
                int failures = resultMission.Count(m => !m.IsSuccess);
                StatVictoryData.AddPoint(successes, failures);

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