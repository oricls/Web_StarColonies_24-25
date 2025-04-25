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

public class ConsultStatsModel : PageModel
{
    private readonly UserManager<Infrastructures.Entities.Colon> _userManager;
    private readonly ITeamRepository _teamRepository;
    private readonly IMissionRepository _missionRepository;
    private readonly ILogger<ConsultStatsModel> _logger;

    public ConsultStatsModel(UserManager<Infrastructures.Entities.Colon> userManager, ITeamRepository teamRepository, IMissionRepository missionRepository, ILogger<ConsultStatsModel> logger)
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
    public int TotalSuccess { get; private set; } = 0;
    public int TotalFailure { get; private set; } = 0;

    public List<StatPoint> StatsEvolution { get; private set; } = new();

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

        if (SelectedTeamId > 0)
        {
            var missionTeam = await _missionRepository.GetMissionsByTeamIdAsync(SelectedTeamId);

            foreach (var mission in missionTeam)
            {
                var resultMission = await _missionRepository.GetResultatsByMissionIdAsync(mission.Id);
                foreach (var result in resultMission)
                {
                    StatsEvolution.Add(new StatPoint
                    {
                        Date = result.Date.ToString("dd-MM-yyyy"),
                        Strength = result.IssueStrength,
                        Endurance = result.IssueEndurance
                    });

                    if (result.IsSuccess) TotalSuccess++;
                    else TotalFailure++;
                }
            }
        }

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            var user = await GetCurrentUserAsync();
            UserTeams = await _teamRepository.GetTeamByColon(user.Id);
            return Page();
        }

        return RedirectToPage(new { SelectedTeamId });
    }
}