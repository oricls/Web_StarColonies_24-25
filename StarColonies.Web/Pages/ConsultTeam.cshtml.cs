using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains;
using StarColonies.Domains.Repositories;

namespace StarColonies.Web.Pages
{
    [Authorize]
    public class ConsultTeamModel : PageModel
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ILogger<ConsultTeamModel> _logger;

        public ConsultTeamModel(
            ITeamRepository teamRepository, 
            ILogger<ConsultTeamModel> logger)
        {
            _teamRepository = teamRepository;
            _logger = logger;
        }

        // Propriétés du modèle
        public Team Team { get; set; }
        public List<Colon> TeamMembers { get; set; } = [];
        public string CurrentUserId { get; set; }
        public bool IsCreator { get; set; }
        public int TotalLevel { get; set; }
        public int TotalForce { get; set; }
        public int TotalEndurance { get; set; }
        public int SuccessfulMissions { get; set; }
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string slug)
        {
            if (string.IsNullOrEmpty(slug))
            {
                _logger.LogWarning("Tentative d'accès sans slug");
                return RedirectToPage("/Dashboard");
            }

            // Récupérer l'ID de l'utilisateur connecté
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("Utilisateur non connecté");
                return RedirectToPage("/Login");
            }

            try
            {
                // Récupérer toutes les équipes
                var allTeams = await _teamRepository.GetAllTeams();
                
                // Afficher toutes les équipes avec leur slug pour débogage
                foreach (var team in allTeams)
                {
                    var teamSlug = team.Name.ToKebab();
                    _logger.LogInformation("Équipe: {TeamName}, ID: {TeamId}, Slug: {TeamSlug}", 
                        team.Name, team.Id, teamSlug);
                }
                
                // Recherche de l'équipe par slug
                foreach (var team in allTeams)
                {
                    if (!team.Name.ToKebab().Equals(slug, StringComparison.OrdinalIgnoreCase)) continue;
                    Team = team;
                    break;
                }
                
                _logger.LogInformation("Équipe trouvée: {TeamName}, ID: {TeamId}", Team.Name, Team.Id);
                
                // Récupérer les membres de l'équipe
                var members = await _teamRepository.GetMembersOfTeam(Team);
                TeamMembers = members.ToList();

                // Vérifier si l'utilisateur est membre de l'équipe
                var currentUserIsTeamMember = TeamMembers.Any(m => m.Id == userId);
                if (!currentUserIsTeamMember)
                {
                    _logger.LogWarning("L'utilisateur n'est pas membre de cette équipe");
                    return RedirectToPage("/Dashboard");
                }

                CurrentUserId = userId;
                // Déterminer si l'utilisateur est le créateur de l'équipe
                IsCreator = userId == Team.CreatorId;

                // Calculer les statistiques de l'équipe
                CalculateTeamStats();

                // Récupérer le nombre de missions réussies (à 0 par défaut)
                // TODO: A changer
                SuccessfulMissions = 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la récupération de l'équipe avec le slug: {Slug}", slug);
                ModelState.AddModelError(string.Empty, $"Erreur lors de la récupération de l'équipe: {ex.Message}");
                return RedirectToPage("/Dashboard");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteTeamAsync(int teamId)
        {
            // Récupérer l'ID de l'utilisateur connecté
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            try
            {
                // Vérifier que l'équipe existe
                var team = await _teamRepository.GetTeamById(teamId);
                
                // Vérifier que l'utilisateur est le créateur de l'équipe
                if (team.CreatorId != userId)
                {
                    return Forbid();
                }
                
                // Supprimer l'équipe
                await _teamRepository.DeleteTeamAsync(team);
                
                return RedirectToPage("/Dashboard");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ConsultTeam.OnPostDeleteTeamAsync {TeamId}", teamId);
                ModelState.AddModelError(string.Empty, $"Erreur lors de la suppression de l'équipe: {ex.Message}");
                
                return RedirectToPage("/Dashboard");
            }
        }

        private void CalculateTeamStats()
        {
            TotalLevel = 0;
            TotalForce = 0;
            TotalEndurance = 0;
            
            foreach (var member in TeamMembers.OfType<Colon>())
            {
                TotalLevel += member.Level;
                TotalForce += member.Level + member.Strength;
                TotalEndurance += member.Level + member.Endurance;
            }
            
        }
    }
}