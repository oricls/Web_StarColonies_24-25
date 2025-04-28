    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using StarColonies.Domains;
    using StarColonies.Domains.Repositories;
    using Colon = StarColonies.Infrastructures.Entities.Colon;

    namespace StarColonies.Web.Pages;

    public class RegisterModel(UserManager<Colon> userManager,
        SignInManager<Colon> signInManagern,
        IColonRepository colonRepository,
        ILogRepository logRepository,
        IWebHostEnvironment webHostEnvironment) : PageModel
    {

        public IEnumerable<Profession> Profession { get; set; } = [];
        
        [BindProperty]
        public RegisterInputModel RegisterInput { get; set; }
        
        [BindProperty]
        public RegisterAvatarInputModel RegisterAvatarInput { get; set; }
        public string AvatarPreview { get; set; }
        public string SelectedAvatarId { get; set; }
        
        [BindProperty]
        public RegisterProfessionInputModel RegisterProfessionInput { get; set; }
        public int SelectedProfession { get; set; }
        public int Force { get; set; }
        public int Endurance { get; set; }
        
        public async Task OnGetAsync()
        {
            Profession = await colonRepository.GetAllProfessionsAsync();
            
            if (Profession.Any())
            {
                SelectedProfession = Profession.First().Id;
            }
        }
        
        private async Task<string> UploadFile(IFormFile file, string subDirectory)
        {
            try
            {
                // Vérifier le fichier
                if (file == null || file.Length == 0)
                {
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
                // Gérer l'erreur d'upload
                return null;
            }
        }
        
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            string avatarPath = "avatars/avatar_5.png"; // Avatar par défaut
            
            if (RegisterAvatarInput.SelectedAvatarId == "import" && RegisterAvatarInput.CustomAvatar != null)
            {
                // Utiliser la méthode UploadFile pour gérer l'upload
                var uploadedPath = await UploadFile(RegisterAvatarInput.CustomAvatar, "avatars");
                if (!string.IsNullOrEmpty(uploadedPath))
                {
                    avatarPath = uploadedPath;
                }
            }
            else if (RegisterAvatarInput.SelectedAvatarId != null)
            {
                // Si un avatar prédéfini est sélectionné
                avatarPath = $"avatars/avatar_{RegisterAvatarInput.SelectedAvatarId}.png";
            }
            
            var colon = new Colon()
            {
                UserName = RegisterInput.NameOfColon,
                NormalizedUserName = RegisterInput.NameOfColon,
                Email = RegisterInput.Courriel,
                NormalizedEmail = RegisterInput.Courriel.ToUpper(),
                DateBirth = RegisterInput.BirthDate,
                Strength = RegisterProfessionInput.Force,
                Endurance = RegisterProfessionInput.Endurance,
                Level = 1,
                Avatar = avatarPath,
                IdProfession = RegisterProfessionInput.SelectedProfession,
            };
            
            var result = await userManager.CreateAsync(colon, RegisterInput.Password);
            
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                
                await logRepository.AddLog(
                    new Log()
                    {
                        RequeteAction = "Inscription",
                        ResponseAction = "Erreur d'inscription : " + string.Join(", ", result.Errors.Select(e => e.Description)),
                        DateHeureAction = DateTime.Now
                    }
                );
                
                return Page();
            }
            
            await signInManagern.SignInAsync(colon, isPersistent: false);
            
            await logRepository.AddLog(
                new Log()
                {
                    RequeteAction = "Inscription",
                    ResponseAction = "Inscription réussie pour " + RegisterInput.NameOfColon,
                    DateHeureAction = DateTime.Now
                }
            );
            

            return RedirectToPage("/DashBoard");
        }
    }
    public class RegisterInputModel
    {
        [Required(ErrorMessage = "Le courriel est requis")]
        [EmailAddress(ErrorMessage = "Format de courriel invalide")]
        [Display(Name = "Courriel")]
        public string Courriel { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le nom de colon est requis")]
        [Display(Name = "Nom de colon")]
        public string NameOfColon { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le mot de passe est requis")]
        [StringLength(100, ErrorMessage = "Le mot de passe doit contenir au moins {2} caractères", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "La confirmation du mot de passe est requise")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmation du mot de passe")]
        [Compare("Password", ErrorMessage = "Les mots de passe ne correspondent pas")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "La date de naissance est requise")]
        [DataType(DataType.Date)]
        [Display(Name = "Date de naissance")]
        public DateTime BirthDate { get; set; }
    }

    public class RegisterAvatarInputModel
    {
        public string? SelectedAvatarId { get; set; }
        
        [Display(Name = "Avatar personnalisé")]
        public IFormFile? CustomAvatar { get; set; }
    }

    public class RegisterProfessionInputModel
    {
        [Required(ErrorMessage = "Veuillez sélectionner une profession")]
        [Display(Name = "Profession")]
        public int SelectedProfession { get; set; }

        [Required(ErrorMessage = "La force est requise")]
        [Range(1, 6, ErrorMessage = "La force doit être comprise entre 1 et 6")]
        [Display(Name = "Force")]
        public int Force { get; set; }
        
        [Required(ErrorMessage = "L'endurance est requise")]
        [Range(1, 6, ErrorMessage = "L'endurance doit être comprise entre 1 et 6")]
        [Display(Name = "Endurance")]
        public int Endurance { get; set; }
    }