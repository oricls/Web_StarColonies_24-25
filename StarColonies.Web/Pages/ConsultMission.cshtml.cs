using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Bestiaire = StarColonies.Domains.Bestiaire;
using Colon = StarColonies.Infrastructures.Entities.Colon;
using Mission = StarColonies.Domains.Mission;

namespace StarColonies.Web.Pages;

public class ConsultMission(
    IMissionRepository missionRepository,
    ITeamRepository teamRepository,
    IColonRepository colonRepository,
    UserManager<Colon> userManager,
    MissionEngine missionEngine,
    ILogger<ConsultMission> logger)
    : PageModel
{
    public Mission Mission { get; private set; } = new();
    
    public IReadOnlyList<Team> UserTeams { get; private set; } = new List<Team>();
    public IReadOnlyList<Bestiaire> Bestiaires { get; private set; } = new List<Bestiaire>();
    
    [BindProperty]
    [Required(ErrorMessage = "Sélectionnez une équipe")]
    [Display(Name = "Équipe")]
    public int SelectedTeamId { get; set; }  // Nouvelle propriété pour la sélection

    public async Task<IActionResult> OnGetAsync(string slug)
    {
        try
        {
            // Récupérer toutes les missions
            var allMissions = await missionRepository.GetAllMissionsAsync();
            
            // Filtrer par slug (convertir le nom en slug et comparer)
            Mission = allMissions.FirstOrDefault(m => m.Name.ToKebab() == slug);
            
            if (Mission == null)
            {
                return NotFound(slug);
            }
            
            // Récupérer les bestiaires pour cette mission
            Bestiaires = await missionRepository.GetBestiairesByMissionIdAsync(Mission.Id);
            
            // Récupérer les teams auxquelles l'utilisateur est associé
            var colon = new Domains.Colon() { Id = userManager.GetUserId(User) };
            UserTeams = await teamRepository.GetTeamByColon(colon);
            
            return Page();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "ConsultMission.OnGet {Slug}", slug);
            return NotFound(slug);
        }
    }
    
    public async Task<IActionResult> OnPostAsync(string slug)
    {
        try
        {
            // 1. Valider la sélection d'équipe
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Sélectionnez une équipe valide.");
                return RedirectToPage("/ConsultMission", new { slug });
            }
            
            // 2. Démarrer la mission pour l'équipe sélectionnée
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/ConsultMission",new { slug });
            }
            
            var colon = await colonRepository.GetColonByIdAsync(userId);
            
            UserTeams = await teamRepository.GetTeamByColon(colon);
            var team = UserTeams.First(t => t.Id == SelectedTeamId);

            var allMissions = await missionRepository.GetAllMissionsAsync();
            Mission = allMissions.FirstOrDefault(m => m.Name.ToKebab() == slug);
            
            if (Mission == null)
            {
                return NotFound(slug);
            }

            var result = missionEngine.ExecuteMission(Mission, team);
            
            missionRepository.SaveMissionResult(result);
            
            // Multiplicateur de 1.5 à 2.5
            if (result.IsSuccess)
            {
                // TODO: Augmenter le niveau de chaque colon de l'équipe
                teamRepository.LevelUpTeam(team);
                
                // Monter les colons de niveau, gagner des ressources...
                return RedirectToPage("/MissionSucessful", new { slug });
            }

            return RedirectToPage("/ResultMissionFailed", new { slug });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "ConsultMission.OnPost {Slug}", slug);
            ModelState.AddModelError("", "Une erreur est survenue");
            return RedirectToPage("/ConsultMission", new { slug });
        }
    }
}