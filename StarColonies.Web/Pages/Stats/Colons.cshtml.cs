using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains;
using StarColonies.Domains.Repositories;

namespace StarColonies.Web.Pages.Stats;

public class ColonStatPoint
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public double Endurance { get; set; } = 0.0;
    public double Strength { get; set; } = 0.0;
    public int Level { get; set; } = 0;
    public string Profession { get; set; } = string.Empty;
    public string Avatar { get; set; } = string.Empty;
    
    // Valeurs de base (sans bonus de niveau)
    public double BaseEndurance { get; set; } = 0.0;
    public double BaseStrength { get; set; } = 0.0;
    
    // Bonus dus au niveau
    public double EnduranceBonus { get; set; } = 0.0;
    public double StrengthBonus { get; set; } = 0.0;
}

public class TeamStatsData
{
    public List<ColonStatPoint> MembersStats { get; set; } = new List<ColonStatPoint>();
    public int TotalChallengesWon { get; set; } = 0;
}

public class ColonsModel(
    UserManager<Infrastructures.Entities.Colon> userManager,
    IColonRepository colonRepository,
    ITeamRepository teamRepository,
    IMissionRepository missionRepository,
    ILogger<ColonsModel> logger)
    : PageModel
{
    public IReadOnlyList<Team> UserTeams { get; private set; } = new List<Team>();
    public IReadOnlyList<Colon> TeamMembers { get; private set; } = new List<Colon>();

    [BindProperty(SupportsGet = true)]
    [Display(Name = "Équipe")]
    public int SelectedTeamId { get; set; }

    [BindProperty(SupportsGet = true)]
    [Display(Name = "Membre de l'équipe")]
    public string? SelectedColonId { get; set; }

    public TeamStatsData TeamStats { get; set; } = new TeamStatsData();
    public ColonStatPoint? SelectedColonStats { get; set; }

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
        try
        {
            var user = await GetCurrentUserAsync();
            UserTeams = await teamRepository.GetTeamByColon(user.Id);
            
            if (SelectedTeamId > 0)
            {
                // Vérifier que l'équipe sélectionnée appartient bien à l'utilisateur
                if (!UserTeams.Any(t => t.Id == SelectedTeamId))
                {
                    ModelState.AddModelError(string.Empty, "Équipe non autorisée");
                    return Page();
                }
                
                var selectedTeam = await teamRepository.GetTeamById(SelectedTeamId);
                if (selectedTeam != null)
                {
                    TeamMembers = await teamRepository.GetMembersOfTeam(selectedTeam);

                    // Préparer les statistiques pour tous les membres de l'équipe
                    foreach (var member in TeamMembers)
                    {
                        var statPoint = new ColonStatPoint
                        {
                            Id = member.Id,
                            Name = member.Name,
                            BaseEndurance = member.Endurance,
                            BaseStrength = member.Strength,
                            Level = member.Level,
                            Profession = member.ProfessionName,
                            Avatar = member.Avatar,
                            
                            // Calculer les bonus et totaux
                            EnduranceBonus = member.Level,
                            StrengthBonus = member.Level
                        };
                        
                        statPoint.Endurance = statPoint.BaseEndurance + statPoint.EnduranceBonus;
                        statPoint.Strength = statPoint.BaseStrength + statPoint.StrengthBonus;
                        
                        TeamStats.MembersStats.Add(statPoint);
                        
                        // Si c'est le colon sélectionné, mettre à jour SelectedColonStats
                        if (member.Id == SelectedColonId)
                        {
                            SelectedColonStats = statPoint;
                        }
                    }
                    
                    // Calculer le nombre total de défis gagnés par l'équipe
                    var teamMissions = await missionRepository.GetMissionsByTeamIdAsync(SelectedTeamId);
                    foreach (var mission in teamMissions)
                    {
                        var allResults = await missionRepository.GetResultatsByMissionIdAsync(mission.Id);
                        var teamResults = allResults.Where(r => r.TeamId == SelectedTeamId).ToList();
                        TeamStats.TotalChallengesWon += teamResults.Count(r => r.IsSuccess);
                    }
                    
                    logger.LogInformation($"Statistiques chargées pour l'équipe {SelectedTeamId} avec {TeamStats.MembersStats.Count} membres. " +
                                         $"Défis gagnés: {TeamStats.TotalChallengesWon}");
                }
                else
                {
                    logger.LogWarning($"Équipe {SelectedTeamId} non trouvée");
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Erreur lors du chargement des statistiques pour l'équipe {SelectedTeamId}");
            ModelState.AddModelError(string.Empty, "Une erreur est survenue lors du chargement des statistiques");
        }
        
        return Page();
    }

    public IActionResult OnPost()
    {
        return RedirectToPage(new { SelectedTeamId });
    }
}