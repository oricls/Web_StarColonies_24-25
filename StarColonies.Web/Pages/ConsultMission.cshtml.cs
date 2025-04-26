using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using StarColonies.Domains.Repositories;
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
    
    public IReadOnlyList<Resource> ResourcesMission { get; private set; } = new List<Resource>();
    
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
            
            // Récupérer les ressources disponibles
            ResourcesMission = await missionRepository.GetResourcesByMissionIdAsync(Mission.Id);
            
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
        
        // 2. Récupérer l'utilisateur et l'équipe
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (string.IsNullOrEmpty(userId))
        {
            return RedirectToPage("/ConsultMission", new { slug });
        }
        
        var colon = await colonRepository.GetColonByIdAsync(userId);
        
        UserTeams = await teamRepository.GetTeamByColon(colon);
        var team = UserTeams.First(t => t.Id == SelectedTeamId);
        
        // 3. Récupérer la mission
        var allMissions = await missionRepository.GetAllMissionsAsync();
        Mission = allMissions.FirstOrDefault(m => m.Name.ToKebab() == slug);
        
        if (Mission == null)
        {
            return NotFound(slug);
        }
        
        // 4. Créer une copie de l'équipe pour y appliquer les bonus
        logger.LogInformation($"Équipe sélectionnée: {team.Name} (ID: {team.Id})");
        logger.LogInformation($"Force initiale: {team.TotalStrength}, Endurance initiale: {team.TotalEndurance}");
        
        var teamWithBonuses = new Team
        {
            Id = team.Id,
            Name = team.Name,
            Logo = team.Logo,
            Baniere = team.Baniere,
            MemberCount = team.MemberCount,
            AverageLevel = team.AverageLevel,
            TotalStrength = team.TotalStrength,
            TotalEndurance = team.TotalEndurance,
            CreatorId = team.CreatorId
        };
        
        // 5. Récupérer et appliquer les bonus actifs de tous les membres de l'équipe
        var teammates = await teamRepository.GetMembersOfTeam(team);
        
        int bonusAppliedCount = 0;
        List<(string UserId, int BonusId)> oneTimeBonusesToExpire = new List<(string, int)>();
        
        foreach (var teammate in teammates)
        {
            var activeBonuses = await colonRepository.GetColonActiveBonusesAsync(teammate.Id);
            
            foreach (var bonus in activeBonuses)
            {
                if (bonus.IsActive()) // TODO : pas sur que ce soit utile mais fonction du domain donc j'aime
                {
                    logger.LogInformation($"Application du bonus: {bonus.Name} (ID: {bonus.Id})");

                    int strengthBefore = teamWithBonuses.TotalStrength;
                    int enduranceBefore = teamWithBonuses.TotalEndurance;

                    
                    // Utiliser la logique de la classe Bonus pour appliquer les effets
                    bonus.ApplyToMission(Mission, teamWithBonuses);
                    bonusAppliedCount++;
                    
                    logger.LogInformation($"Force: {strengthBefore} -> {teamWithBonuses.TotalStrength}");
                    logger.LogInformation($"Endurance: {enduranceBefore} -> {teamWithBonuses.TotalEndurance}");

                    //TODO : to DB or not to DB ?
                    if (bonus.Name.Contains("usage unique"))
                    {
                        oneTimeBonusesToExpire.Add((teammate.Id, bonus.Id));
                    }
                }
            }
        }
        
        logger.LogInformation($"Total de {bonusAppliedCount} bonus appliqués");
        logger.LogInformation($"Force finale: {teamWithBonuses.TotalStrength}, Endurance finale: {teamWithBonuses.TotalEndurance}");
        
        // 6. Exécuter la mission avec l'équipe modifiée par les bonus
        var result = missionEngine.ExecuteMission(Mission, teamWithBonuses);
        
        // Expirer les bonus à usage unique identifiés
        foreach (var (curUserId, bonusId) in oneTimeBonusesToExpire)
        {
            await colonRepository.ExpireBonusAsync(curUserId, bonusId);
            logger.LogInformation($"Bonus à usage unique {bonusId} expiré après utilisation pour l'utilisateur {userId}");
        }
        
        missionRepository.SaveMissionResult(result);
        
        // 7. Rediriger en fonction du résultat
        return !result.IsSuccess 
            ? RedirectToPage("/ResultMissionFailed", new { slug }) 
            : RedirectToPage("/MissionSucessful", new { idMission = Mission.Id, teamId = team.Id });
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "ConsultMission.OnPost {Slug}", slug);
        ModelState.AddModelError("", "Une erreur est survenue");
        return RedirectToPage("/ConsultMission", new { slug });
    }
}
}