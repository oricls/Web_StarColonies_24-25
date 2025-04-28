using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains;
using StarColonies.Domains.Repositories;

namespace StarColonies.Web.Pages.Stats;

public class StatPoint
{
    public string Date { get; set; }
    public double Endurance { get; set; }
    public double Strength { get; set; }
}

public class VictoryData
{
    public int TotalSuccess { get; set; } = 0;
    public int TotalFailure { get; set; } = 0;
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
    public VictoryData StatVictoryData { get; private set; } = new VictoryData();

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
        try
        {
            var user = await GetCurrentUserAsync();
            UserTeams = await teamRepository.GetTeamByColon(user.Id);

            if (SelectedTeamId > 0)
            {
                // Vérifier que l'équipe sélectionnée appartient bien à l'utilisateur
                if (UserTeams.All(t => t.Id != SelectedTeamId))
                {
                    ModelState.AddModelError(string.Empty, "Équipe non autorisée");
                    return Page();
                }

                // Récupérer les missions de l'équipe sélectionnée
                var missionTeam = await missionRepository.GetMissionsByTeamIdAsync(SelectedTeamId);
                
                int totalSuccess = 0;
                int totalFailure = 0;
                var statsPoints = new List<StatPoint>();
                
                foreach (var mission in missionTeam)
                {
                    // Récupérer tous les résultats pour cette mission
                    var allMissionResults = await missionRepository.GetResultatsByMissionIdAsync(mission.Id);
                    
                    // Filtrer pour n'obtenir que les résultats de l'équipe sélectionnée
                    var teamResults = allMissionResults.Where(r => r.TeamId == SelectedTeamId).ToList();
                    
                    // Ajouter des points de statistiques
                    foreach (var result in teamResults)
                    {
                        logger.LogDebug($"Ajout stats pour mission {mission.Id}, équipe {SelectedTeamId}, " +
                                       $"date {result.Date}, force {result.IssueStrength}, endurance {result.IssueEndurance}");
                        
                        statsPoints.Add(new StatPoint
                        {
                            Date = result.Date.ToString("dd-MM-yyyy"),
                            Strength = result.IssueStrength,
                            Endurance = result.IssueEndurance
                        });
                    }

                    // Calculer les succès et échecs pour cette mission et cette équipe
                    int successes = teamResults.Count(m => m.IsSuccess);
                    int failures = teamResults.Count(m => !m.IsSuccess);
                    
                    totalSuccess += successes;
                    totalFailure += failures;
                    
                    logger.LogInformation($"Mission {mission.Id}: {successes} succès, {failures} échecs pour l'équipe {SelectedTeamId}");
                }
                
                StatVictoryData.TotalSuccess = totalSuccess;
                StatVictoryData.TotalFailure = totalFailure;
                
                // Trier les points de statistiques par date
                if (statsPoints.Any())
                {
                    StatsEvolution = statsPoints
                        .OrderBy(s => DateTime.ParseExact(s.Date, "dd-MM-yyyy", CultureInfo.InvariantCulture))
                        .ToList();
                    
                    logger.LogInformation($"Généré {StatsEvolution.Count} points de statistiques pour l'équipe {SelectedTeamId}");
                }
                else
                {
                    logger.LogWarning($"Aucun point de statistique trouvé pour l'équipe {SelectedTeamId}");
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Erreur lors de la récupération des statistiques pour l'équipe {SelectedTeamId}");
            ModelState.AddModelError(string.Empty, "Une erreur est survenue lors du chargement des statistiques");
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