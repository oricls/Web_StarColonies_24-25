using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains;
using StarColonies.Domains.Repositories;
using StarColonies.Web.Validators;

namespace StarColonies.Web.Pages
{
    [Authorize]
    public class CreateTeamModel(ITeamRepository teamRepository, IColonRepository colonRepository, ILogRepository logRepository)
        : PageModel
    {
        // Propriétés liées au formulaire
        [BindProperty]
        [Required(ErrorMessage = "Le nom de l'équipe est obligatoire.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Le nom de l'équipe doit contenir entre 3 et 50 caractères.")]
        [Display(Name = "Nom de l'équipe")]
        public string TeamName { get; set; }

        [Display(Name = "Logo de l'équipe")]
        public string Logo { get; set; } = "default_logo.png";

        [Display(Name = "Bannière de l'équipe")]
        public string Baniere { get; set; } = "default_baniere.png";

        // ID du créateur (utilisateur connecté)
        [BindProperty]
        public string CreatorId { get; set; }

        // Liste des colons sélectionnés pour faire partie de l'équipe
        [BindProperty]
        [Display(Name = "Membres de l'équipe")]
        public List<string> SelectedColonIds { get; set; } = [];

        // Nouveau champ pour la validation par professions
        [BindProperty]
        [ProfessionCompositionValidator(ErrorMessage = "La composition de l'équipe n'est pas valide.")]
        public string SelectedProfessions { get; set; }

        // Liste des colons disponibles - maintenu pour la rétrocompatibilité
        public IEnumerable<Colon> AvailableColons { get; set; } = new List<Colon>();

        // Informations sur l'utilisateur connecté
        public Colon CurrentUser { get; set; }

        // Regroupement des colons par profession
        public Dictionary<string, List<Colon>> ColonsByProfession { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            // Récupérer l'ID de l'utilisateur connecté
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Login");
            }

            CreatorId = userId;

            // Récupérer les informations de l'utilisateur connecté
            CurrentUser = await colonRepository.GetColonByIdAsync(userId);

            // Récupérer tous les colons disponibles pour former une équipe
            var allColons = await colonRepository.GetAllColonsAsync();
            AvailableColons = allColons.Where(c => c.Id != userId); // Exclure l'utilisateur connecté

            // Initialiser SelectedColonIds comme une liste vide
            SelectedColonIds = new List<string>();

            // Regrouper les colons par profession
            ColonsByProfession = AvailableColons
                .GroupBy(c => c.ProfessionName)
                .ToDictionary(g => g.Key, g => g.ToList());

            // Initialiser le champ SelectedProfessions avec la profession du CurrentUser
            SelectedProfessions = JsonSerializer.Serialize(new List<string> { CurrentUser.ProfessionName });

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Récupérer l'ID de l'utilisateur connecté
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            // Récupérer les informations de l'utilisateur connecté
            if (userId != null)
            {
                CurrentUser = await colonRepository.GetColonByIdAsync(userId);

                // Récupérer tous les colons disponibles
                var allColons = await colonRepository.GetAllColonsAsync();
                AvailableColons = allColons.Where(c => c.Id != userId);
            }

            // Regrouper les colons par profession
            var availableColons = AvailableColons.ToList();
            ColonsByProfession = availableColons
                .GroupBy(c => c.ProfessionName)
                .ToDictionary(g => g.Key, g => g.ToList());
            
            // Mettre à jour SelectedProfessions avec les valeurs actuelles
            var professions = new List<string> { CurrentUser.ProfessionName };
            professions.AddRange(SelectedColonIds
                .Select(colonId => availableColons
                    .FirstOrDefault(c => c.Id == colonId))
                .OfType<Colon>()
                .Select(colon => colon.ProfessionName));

            SelectedProfessions = JsonSerializer.Serialize(professions);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                // Créer l'équipe
                var team = new Team
                {
                    Name = TeamName,
                    Logo = Logo,
                    Baniere = Baniere,
                    CreatorId = CreatorId
                };

                // Enregistrer l'équipe dans la base de données
                await teamRepository.CreateTeamAsync(team);

                // Ajouter les membres à l'équipe (y compris le créateur)
                await teamRepository.AddMemberToTeamAsync(team.Id, CreatorId);
                
                foreach (var colonId in SelectedColonIds)
                {
                    await teamRepository.AddMemberToTeamAsync(team.Id, colonId);
                }
                
                await logRepository.AddLog(
                    new Log
                    {
                        RequeteAction = "Création d'équipe",
                        ResponseAction = $"Équipe '{TeamName}' créée avec succès.",
                        DateHeureAction = DateTime.Now
                    }
                );

                return RedirectToPage("/Dashboard");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Erreur lors de la création de l'équipe: {ex.Message}");
                
                await logRepository.AddLog(
                    new Log
                    {
                        RequeteAction = "Création d'équipe",
                        ResponseAction = $"Erreur lors de la création de l'équipe: {ex.Message}",
                        DateHeureAction = DateTime.Now
                    }
                );
                return Page();
            }
        }
    }
}