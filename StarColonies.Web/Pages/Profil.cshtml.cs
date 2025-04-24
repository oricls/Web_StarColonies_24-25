using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains;
using StarColonies.Domains.Repositories;

namespace StarColonies.Web.Pages;

public class UpdateProfilViewModel
{
    [Required(ErrorMessage = "Le courriel est requis")]
    [EmailAddress(ErrorMessage = "Format du courriel invalide")]
    [Display(Name = "Courriel")]
    public string Courriel { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Le nom de colon est requis")]
    [Display(Name = "Nom de colon")]
    public string NomDeColon { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "La date de naissance est requise")]
    [DataType(DataType.Date)]
    [Display(Name = "Date de naissance")]
    public DateTime DateDeNaissance { get; set; } = DateTime.Today;
    
    [Display(Name = "Nouveau mot de passe")]
    [DataType(DataType.Password)]
    public string NouveauMotDePasse { get; set; }  = string.Empty;
    
    [Display(Name = "Confirmation mot de passe")]
    [DataType(DataType.Password)]
    [Compare("NouveauMotDePasse", ErrorMessage = "Les mots de passe ne correspondent pas")]
    public string ConfirmationMotDePasse { get; set; }  = string.Empty;
    
    public IFormFile? UploadAvatar { get; set; }
    
    public string Avatar { get; set; } = string.Empty;
}


public class Profil : PageModel
{
    private readonly IColonRepository _repository;
    private readonly ILogger<Profil> _logger;
    private readonly UserManager<Infrastructures.Entities.Colon> _userManager;
    public Colon Colon { get; private set; }

    [BindProperty]
    public UpdateProfilViewModel UpdateProfil { get; set; } = new UpdateProfilViewModel();
    
    public Profil(IColonRepository colonRepository, UserManager<Infrastructures.Entities.Colon> userManager, ILogger<Profil> logger)
    {
        _userManager = userManager;
        _repository = colonRepository;
        _logger = logger;
    }

    public async Task<IActionResult> OnGet()
    {
        var user = await _userManager.GetUserAsync(User);
        try
        {
            Colon = await _repository.GetColonByIdAsync(user.Id);
            UpdateProfil = new UpdateProfilViewModel
            {
                Courriel = Colon.Email,
                NomDeColon = Colon.Name,
                DateDeNaissance = Colon.DateBirth,
                Avatar = Colon.Avatar
            };
            
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Profile - Erreur lors de la récupération de l'utilisateur");
            return Page();
        }
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        var user = await _userManager.GetUserAsync(User);
        try
        {
            var colon = await _repository.GetColonByIdAsync(user.Id);
            colon.Name = UpdateProfil.NomDeColon;
            colon.Email = UpdateProfil.Courriel;
            colon.DateBirth = UpdateProfil.DateDeNaissance;

            await _repository.UpdateColonAsync(colon);
            
            if (!string.IsNullOrEmpty(UpdateProfil.NouveauMotDePasse))
            { 
                await _repository.ChangePassword(user.Id, UpdateProfil.ConfirmationMotDePasse);
            }

            Colon = await _repository.GetColonByIdAsync(user.Id);
            UpdateProfil = new UpdateProfilViewModel
            {
                Courriel = Colon.Email,
                NomDeColon = Colon.Name,
                DateDeNaissance = Colon.DateBirth,
                Avatar = Colon.Avatar
            };
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Profil - Erreur lors de la mise à jour du profil");
            return Page();
        }
    }

    public async Task<IActionResult> OnPostDeleteAccountAsync()
    {
        // TODO : plutot ajt message de confrimation avant de suppr
        var user = await _userManager.GetUserAsync(User);
        try
        {
            await _repository.DeleteColonAsync(user.Id); 
            return RedirectToPage("/Index");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}