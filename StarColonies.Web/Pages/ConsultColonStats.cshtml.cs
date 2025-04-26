using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains;
using StarColonies.Domains.Repositories;

namespace StarColonies.Web.Pages;

public class ColonStatPoint
{
    public double Endurance { get; set; } = 0.0;
    public double Strength { get; set; } = 0.0;
    public int Level { get; set; }= 0;
    public int ChallengeWon { get; set; } = 0; 
}

public class ConsultColonStats(
    UserManager<Infrastructures.Entities.Colon> userManager,
    IColonRepository colonRepository,
    ITeamRepository teamRepository,
    IMissionRepository missionRepository,
    ILogger<ConsultStatsModel> logger)
    : PageModel
{
    public IReadOnlyList<Team> UserTeams { get; private set; } = new List<Team>();
    public IReadOnlyList<Colon> TeamMembers { get; private set; }  = new List<Colon>();

    [BindProperty(SupportsGet = true)]
    [Display(Name = "Équipe")]
    public int SelectedTeamId { get; set; }

    [BindProperty(SupportsGet = true)]
    [Display(Name = "Membre de l'équipe")]
    public string? SelectedColonId { get; set; }

    public ColonStatPoint StatsEvolution { get; set; } = new ColonStatPoint();
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
            var selectedTeam = await teamRepository.GetTeamById(SelectedTeamId);
            if (selectedTeam != null)
            {
                TeamMembers = await teamRepository.GetMembersOfTeam(selectedTeam);

                if (!string.IsNullOrEmpty(SelectedColonId))
                {
                    var colon = await colonRepository.GetColonByIdAsync(SelectedColonId);
                    if (colon != null)
                    {
                        StatsEvolution = new ColonStatPoint()
                        {
                            Endurance = colon.Endurance,
                            Strength = colon.Strength,
                            Level = colon.Level,
                        };
                    }
                }
            }
            
            int totalReussites = 0;
            foreach (var mission in await missionRepository.GetMissionsByTeamIdAsync(SelectedTeamId))
            {
                var resultats = await missionRepository.GetResultatsByMissionIdAsync(mission.Id);
                totalReussites += resultats.Count(r => r.IsSuccess);
            }
            StatsEvolution.ChallengeWon = totalReussites;
        }
        else
        {
            TeamMembers = new List<Colon>();
            var currentUser = await colonRepository.GetColonByIdAsync(SelectedColonId);
            if (currentUser != null && !UserTeams.Any())
            {
                StatsEvolution = new ColonStatPoint()
                {
                    Endurance = currentUser.Endurance,
                    Strength = currentUser.Strength,
                    Level = currentUser.Level,
                };
            }
        }
        return Page();
    }

    public IActionResult OnPost()
    {
        return RedirectToPage(new { SelectedTeamId });
    }
}