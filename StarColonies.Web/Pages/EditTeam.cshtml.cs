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
    public class EditTeamModel(
        ITeamRepository teamRepository,
        IColonRepository colonRepository,
        ILogRepository logRepository,
        ILogger<EditTeamModel> logger,
        IWebHostEnvironment webHostEnvironment)
        : PageModel
    {
        // Propriétés liées au formulaire
        [BindProperty]
        [Required(ErrorMessage = "Le nom de l'équipe est obligatoire.")]
        [StringLength(50, MinimumLength = 3,
            ErrorMessage = "Le nom de l'équipe doit contenir entre 3 et 50 caractères.")]
        [Display(Name = "Nom de l'équipe")]
        public string TeamName { get; set; }

        [BindProperty] public int TeamId { get; set; }

        [BindProperty]
        [Display(Name = "Logo de l'équipe")]
        public string Logo { get; set; } = "img/rocket.png";

        [BindProperty]
        [Display(Name = "Bannière de l'équipe")]
        public string Baniere { get; set; } = "img/rocket.png";

        // Fichiers uploadés - Pas de BindProperty ici pour éviter le modèle vide
        public IFormFile LogoFile { get; set; }
        public IFormFile BaniereFile { get; set; }

        // ID du créateur (utilisateur connecté)
        [BindProperty] public string CreatorId { get; set; }

        // Liste des colons sélectionnés pour faire partie de l'équipe
        [BindProperty]
        [Display(Name = "Membres de l'équipe")]
        public List<string> SelectedColonIds { get; set; } = [];

        // Nouveau champ pour la validation par professions
        [BindProperty]
        [ProfessionCompositionValidator(ErrorMessage = "La composition de l'équipe n'est pas valide.")]
        public string SelectedProfessions { get; set; }

        // Membres actuels de l'équipe
        public List<Colon> CurrentMembers { get; set; } = [];

        // Liste des colons disponibles (qui ne font pas partie de l'équipe)
        public IEnumerable<Colon> AvailableColons { get; set; } = new List<Colon>();

        // Informations sur l'utilisateur connecté
        public Colon CurrentUser { get; set; }

        // Regroupement des colons par profession
        public Dictionary<string, List<Colon>> ColonsByProfession { get; set; } = new();

        // Slug original pour le retour à la page de consultation
        public string OriginalSlug { get; set; }

        // État du traitement
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string slug)
        {
            ModelState.Remove("LogoFile");
            ModelState.Remove("BaniereFile");

            if (string.IsNullOrEmpty(slug))
            {
                logger.LogWarning("Tentative d'accès sans slug");
                return RedirectToPage("/Dashboard");
            }

            // Récupérer l'ID de l'utilisateur connecté
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                logger.LogWarning("Utilisateur non connecté");
                return RedirectToPage("/Login");
            }

            try
            {
                // Récupérer toutes les équipes
                var allTeams = await teamRepository.GetAllTeams();

                // Recherche de l'équipe par slug
                var team = allTeams.FirstOrDefault(t => t.Name
                    .ToKebab()
                    .Equals(slug, StringComparison.OrdinalIgnoreCase)) ?? throw new InvalidOperationException();

                // Vérifier si l'utilisateur est le créateur de l'équipe
                if (team.CreatorId != userId)
                {
                    logger.LogWarning("L'utilisateur n'est pas le créateur de cette équipe");
                    return RedirectToPage("/ConsultTeam", new { slug });
                }

                // Récupérer l'utilisateur actuel
                CurrentUser = await colonRepository.GetColonByIdAsync(userId);

                // Initialiser les propriétés du modèle
                TeamId = team.Id;
                TeamName = team.Name;
                CreatorId = team.CreatorId;
                Logo = team.Logo;

                // Vérifier si la bannière existe, sinon utiliser le logo
                Baniere = !string.IsNullOrEmpty(team.Baniere) ? team.Baniere : team.Logo;

                OriginalSlug = slug;

                // Récupérer les membres actuels de l'équipe
                var members = await teamRepository.GetMembersOfTeam(team);
                CurrentMembers = members.ToList();

                // Initialiser les ID des membres actuels pour la sélection
                SelectedColonIds = CurrentMembers
                    .Where(m => m.Id != userId) // Exclure l'utilisateur connecté
                    .Select(m => m.Id)
                    .ToList();

                // Récupérer tous les colons disponibles qui ne sont pas déjà dans l'équipe
                var allColons = await colonRepository.GetAllColonsAsync();
                AvailableColons = allColons.Where(c => CurrentMembers.All(m => m.Id != c.Id));

                // Regrouper les colons disponibles par profession
                ColonsByProfession = AvailableColons
                    .GroupBy(c => c.ProfessionName)
                    .ToDictionary(g => g.Key, g => g.ToList());

                // Initialiser SelectedProfessions avec les professions actuelles
                var professions = CurrentMembers.Select(m => m.ProfessionName).Distinct().ToList();
                SelectedProfessions = JsonSerializer.Serialize(professions);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erreur lors de la récupération de l'équipe avec le slug: {Slug}", slug);
                ModelState.AddModelError(string.Empty, $"Erreur lors de la récupération de l'équipe: {ex.Message}");
                return RedirectToPage("/Dashboard");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Récupérer l'ID de l'utilisateur connecté
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Login");
            }

            // Enlever ces champs de la validation du modèle
            ModelState.Remove("LogoFile");
            ModelState.Remove("BaniereFile");

            // Obtenir les fichiers directement du formulaire
            LogoFile = Request.Form.Files.GetFile("LogoFile");
            BaniereFile = Request.Form.Files.GetFile("BaniereFile");

            // Vérifier que l'utilisateur est bien le créateur de l'équipe
            var team = await teamRepository.GetTeamById(TeamId);
            if (team == null || team.CreatorId != userId)
            {
                ModelState.AddModelError(string.Empty, "Vous n'êtes pas autorisé à modifier cette équipe.");
                return RedirectToPage("/Dashboard");
            }

            // Récupérer les informations de l'utilisateur connecté
            CurrentUser = await colonRepository.GetColonByIdAsync(userId);

            // Récupérer les membres actuels de l'équipe
            var currentMembers = await teamRepository.GetMembersOfTeam(team);
            CurrentMembers = currentMembers.ToList();

            // Récupérer tous les colons disponibles
            var allColons = await colonRepository.GetAllColonsAsync();
            AvailableColons = allColons.Where(c => CurrentMembers.All(m => m.Id != c.Id));

            // Regrouper les colons par profession
            ColonsByProfession = AvailableColons
                .GroupBy(c => c.ProfessionName)
                .ToDictionary(g => g.Key, g => g.ToList());

            // Mettre à jour SelectedProfessions avec les valeurs actuelles
            List<string> professions =
            [
                CurrentUser.ProfessionName // Le créateur reste toujours dans l'équipe

                // Ajouter les professions des membres sélectionnés
            ];

            // Ajouter les professions des membres actuels qui ne peuvent pas être retirés

            // Ajouter les professions des membres sélectionnés
            foreach (var colonId in SelectedColonIds)
            {
                var colon = allColons.FirstOrDefault(c => c.Id == colonId);
                if (colon != null && !professions.Contains(colon.ProfessionName))
                {
                    professions.Add(colon.ProfessionName);
                }
            }

            SelectedProfessions = JsonSerializer.Serialize(professions);

            // Conserver le logo et la bannière existants
            Logo = team.Logo;
            Baniere = string.IsNullOrEmpty(team.Baniere) ? team.Logo : team.Baniere;

            // Conserver le slug original pour le retour à la page de consultation
            OriginalSlug = team.Name.ToKebab();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                // Mettre à jour le nom de l'équipe
                team.Name = TeamName;

                // Traiter le fichier du logo s'il est fourni
                if (LogoFile != null && LogoFile.Length > 0)
                {
                    logger.LogInformation("Upload de nouveau logo: {Filename}", LogoFile.FileName);
                    string logoPath = await UploadFile(LogoFile, "logos");
                    if (!string.IsNullOrEmpty(logoPath))
                    {
                        team.Logo = logoPath;
                        Logo = logoPath;
                    }
                }

                // Traiter le fichier de la bannière s'il est fourni
                if (BaniereFile != null && BaniereFile.Length > 0)
                {
                    logger.LogInformation("Upload de nouvelle bannière: {Filename}", BaniereFile.FileName);
                    var banierePath = await UploadFile(BaniereFile, "banieres");
                    if (!string.IsNullOrEmpty(banierePath))
                    {
                        team.Baniere = banierePath;
                        Baniere = banierePath;
                    }
                }

                // Enregistrer les modifications
                teamRepository.UpdateTeamInfo(team);

                // Déterminer les membres à ajouter et à supprimer
                var currentMemberIds = CurrentMembers.Select(m => m.Id).ToList();
                var membersToRemove = currentMemberIds
                    .Where(id => id != userId && !SelectedColonIds.Contains(id))
                    .ToList();
                var membersToAdd = SelectedColonIds
                    .Where(id => !currentMemberIds.Contains(id))
                    .ToList();

                // Supprimer les membres retirés
                foreach (var colonId in membersToRemove)
                {
                    // Récupérer le colon à partir de l'ID
                    var colon = await colonRepository.GetColonByIdAsync(colonId);
                    teamRepository.RemoveMemberToTeam(team, colon);
                }

                // Ajouter les nouveaux membres
                foreach (var colonId in membersToAdd)
                {
                    await teamRepository.AddMemberToTeamAsync(team.Id, colonId);
                }

                StatusMessage = "Équipe mise à jour avec succès !";
                
                await logRepository.AddLog(
                    new Log()
                    {
                        RequeteAction = "Mise à jour de l'équipe",
                        ResponseAction = $"Équipe mise à jour: {TeamName}",
                        DateHeureAction = DateTime.Now
                    }
                );

                // Rediriger vers la page de consultation avec le nouveau slug
                return RedirectToPage("/ConsultTeam", new { slug = TeamName.ToKebab() });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erreur lors de la mise à jour de l'équipe: {TeamId}", TeamId);
                ModelState.AddModelError(string.Empty, $"Erreur lors de la mise à jour de l'équipe: {ex.Message}");
                
                await logRepository.AddLog(
                    new Log()
                    {
                        RequeteAction = "Mise à jour de l'équipe",
                        ResponseAction = $"Erreur lors de la mise à jour de l'équipe: {ex.Message}",
                        DateHeureAction = DateTime.Now
                    }
                );
                return Page();
            }
        }

        // Méthode pour uploader un fichier et retourner le chemin
        private async Task<string> UploadFile(IFormFile file, string subDirectory)
        {
            try
            {
                // Vérifier le fichier
                if (file == null || file.Length == 0)
                {
                    logger.LogWarning("Tentative d'upload d'un fichier vide ou nul");
                    return null;
                }

                // Créer le répertoire s'il n'existe pas
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "uploads", subDirectory);
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Générer un nom de fichier unique
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Sauvegarder le fichier
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                // Retourner le chemin relatif pour le stockage en base de données
                return $"uploads/{subDirectory}/{uniqueFileName}";
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erreur lors de l'upload du fichier: {FileName}", file?.FileName);
                return null;
            }
        }
    }
}