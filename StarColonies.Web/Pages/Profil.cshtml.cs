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


public class Profil(
    IColonRepository colonRepository,
    UserManager<Infrastructures.Entities.Colon> userManager,
    ILogRepository logRepository,
    ILogger<Profil> logger)
    : PageModel
{
    public Colon Colon { get; private set; }

    [BindProperty]
    public UpdateProfilViewModel UpdateProfil { get; set; } = new UpdateProfilViewModel();
    
    private Infrastructures.Entities.Colon? _user;

    private async Task<Infrastructures.Entities.Colon> GetCurrentUserAsync()
    {
        _user = await userManager.GetUserAsync(User);
        if (_user == null)
        {
            throw new ApplicationException("User impossible à obtenir");
        }
        return _user;
    }
    
    
    public async Task<IActionResult> OnGet()
    {
        try
        {
            var user = await GetCurrentUserAsync();
            Colon = await colonRepository.GetColonByIdAsync(user.Id);
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
            logger.LogError(ex, "Profile - Erreur lors de la récupération de l'utilisateur");
            return Page();
        }
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        try
        {
            var user =  await GetCurrentUserAsync();
            var colon = await colonRepository.GetColonByIdAsync(user.Id);
            colon.Name = UpdateProfil.NomDeColon;
            colon.Email = UpdateProfil.Courriel;
            colon.DateBirth = UpdateProfil.DateDeNaissance;

            await colonRepository.UpdateColonAsync(colon);
            
            await logRepository.AddLog(
                new Log()
                {
                    RequeteAction = "Mise à jour du profil",
                    ResponseAction = "Profil mis à jour avec succès",
                    DateHeureAction = DateTime.Now
                }
            );
            
            if (!string.IsNullOrEmpty(UpdateProfil.NouveauMotDePasse))
            { 
                await colonRepository.ChangePassword(user.Id, UpdateProfil.ConfirmationMotDePasse);
            }

            Colon = await colonRepository.GetColonByIdAsync(user.Id);
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
            logger.LogError(ex, "Profil - Erreur lors de la mise à jour du profil");
            return Page();
        }
    }

    public async Task<IActionResult> OnPostDeleteAccountAsync()
    {
        try
        {
            var user =  await GetCurrentUserAsync();
            await colonRepository.DeleteColonAsync(user.Id); 
            
            await logRepository.AddLog(
                new Log()
                {
                    RequeteAction = "Suppression de compte",
                    ResponseAction = "Compte supprimé avec succès",
                    DateHeureAction = DateTime.Now
                }
            );
            
            return RedirectToPage("/Index");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}