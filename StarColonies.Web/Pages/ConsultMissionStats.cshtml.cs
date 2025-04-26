using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains;
using StarColonies.Domains.Repositories;

namespace StarColonies.Web.Pages;

public class TeamStatEvolution
{
    public int DifficultyLevel { get; set; }
    public int SuccessRate { get; set; }
    public int ChallengesEncountered { get; set; }
}

public class ConsultMissionStats(
    UserManager<Infrastructures.Entities.Colon> userManager,
    ITeamRepository teamRepository,
    IMissionRepository missionRepository,
    ILogger<ConsultStatsModel> logger)
    : PageModel
{

    [BindProperty(SupportsGet = true)]
    [Display(Name = "Équipe")]
    public int SelectedTeamId { get; set; }
    public IReadOnlyList<Team> UserTeams { get; private set; } = new List<Team>();
    
    
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
        
        return Page();
    }
    
    public async Task<IActionResult> OnPost()
    {
        return Page();
    }
}