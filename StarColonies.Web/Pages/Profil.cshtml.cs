using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains;

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
    private readonly ILogger<ConsultMission> _logger;
    public Colon User { get; private set; }

    [BindProperty]
    public UpdateProfilViewModel UpdateProfil { get; set; } = new UpdateProfilViewModel();
    
    private const string ColonId = "e98d7af7-b9be-4877-a56b-5a336f83853b"; // TODO a modifier
    
    public Profil(IColonRepository colonRepository, ILogger<ConsultMission> logger)
    {
        _repository = colonRepository;
        _logger = logger;
    }

    public async Task<IActionResult> OnGet()
    {
        try
        {
            User = await _repository.GetColonByIdAsync(ColonId);
            UpdateProfil = new UpdateProfilViewModel
            {
                Courriel = User.Email,
                NomDeColon = User.Name,
                DateDeNaissance = User.DateBirth,
                Avatar = User.Avatar
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
        try
        {
            var colon = await _repository.GetColonByIdAsync(ColonId);
            colon.Name = UpdateProfil.NomDeColon;
            colon.Email = UpdateProfil.Courriel;
            colon.DateBirth = UpdateProfil.DateDeNaissance;

            await _repository.UpdateColonAsync(colon);
            
            if (!string.IsNullOrEmpty(UpdateProfil.NouveauMotDePasse))
            { 
                await _repository.ChangePassword(ColonId, UpdateProfil.ConfirmationMotDePasse);
            }

            User = await _repository.GetColonByIdAsync(ColonId);
            UpdateProfil = new UpdateProfilViewModel
            {
                Courriel = User.Email,
                NomDeColon = User.Name,
                DateDeNaissance = User.DateBirth,
                Avatar = User.Avatar
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
        try
        {
            await _repository.DeleteColonAsync(ColonId); 
            return RedirectToPage("/Index");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}