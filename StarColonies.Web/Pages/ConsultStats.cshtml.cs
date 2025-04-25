using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains;
using StarColonies.Domains.Repositories;

namespace StarColonies.Web.Pages;

public class ConsultStatsModel : PageModel
{
    private readonly UserManager<Infrastructures.Entities.Colon> _userManager;
    private readonly ITeamRepository _teamRepository;
    private readonly IMissionRepository _missionRepository;
    private readonly ILogger<ConsultStatsModel> _logger;

    public ConsultStatsModel(UserManager<Infrastructures.Entities.Colon> userManager, IColonRepository colonRepository, IMissionRepository missionRepository, ITeamRepository teamRepository, ILogger<ConsultStatsModel> logger)
    {
        _userManager = userManager;
        _teamRepository = teamRepository;
        _missionRepository = missionRepository;
        _logger = logger;
    }
    
    public IReadOnlyList<Team> UserTeams { get; private set; } = new List<Team>();

    [BindProperty(SupportsGet = true)]
    [Display(Name = "Équipe")]
    public int SelectedTeamId { get; set; }
    public int TotalSuccess { get; set; } = 0;
    public int TotalFailure { get; set; } = 0;

    
    private async Task<Infrastructures.Entities.Colon> GetCurrentUserAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            throw new ApplicationException("User impossible à obtenir");
        }
        return user;
    }

    public async Task<IActionResult> OnGet()
    {
        var user = await GetCurrentUserAsync();
        UserTeams = await _teamRepository.GetTeamByColon(user.Id);

        foreach (var team in UserTeams)
        {
            var missionTeam = _missionRepository.GetMissionsByTeamId(team.Id);

            foreach (var mission in missionTeam)
            {
                var resultMission = _missionRepository.GetResultatsByMissionId(mission.Id);
                foreach (var result in resultMission) // pourquoi une liste ????
                {
                    if (result.IsSuccess)
                    {
                        TotalSuccess++;
                    }
                    else
                    {
                        TotalFailure++;
                    }
                }
            }
        }

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            return RedirectToPage(new { SelectedTeamId = SelectedTeamId });
        }

        var user = await GetCurrentUserAsync();
        var teamsResult = await _teamRepository.GetTeamByColon(user.Id);
        UserTeams = teamsResult.ToList();
        return Page();
    }
}