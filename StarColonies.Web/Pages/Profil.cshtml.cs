using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains;

namespace StarColonies.Web.Pages;

public class UpdateProfilViewModel
{
    public string Courriel { get; set; } = string.Empty;
    public string NomDeColon { get; set; }  = string.Empty;
    public DateTime DateDeNaissance { get; set; }  = DateTime.Today;
    public string NouveauMotDePasse { get; set; }  = string.Empty;
    public string ConfirmationMotDePasse { get; set; } = string.Empty;
    public string AvatarActuel { get; set; } = string.Empty;
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
                AvatarActuel = User.Avatar
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
        if (UpdateProfil.NouveauMotDePasse != UpdateProfil.ConfirmationMotDePasse)
        {
            ModelState.AddModelError(UpdateProfil.ConfirmationMotDePasse, "Le nouveau mot de passe et la confirmation ne correspondent pas.");
            return Page();
        }

        try
        {
            var colon = await _repository.GetColonByIdAsync(ColonId);
            colon.Name = UpdateProfil.NomDeColon;
            colon.Email = UpdateProfil.Courriel;
            colon.DateBirth = UpdateProfil.DateDeNaissance;

            await _repository.UpdateColonAsync(colon);
            await _repository.ChangePassword(ColonId, UpdateProfil.ConfirmationMotDePasse);

            User = await _repository.GetColonByIdAsync(ColonId);
            UpdateProfil = new UpdateProfilViewModel
            {
                Courriel = User.Email,
                NomDeColon = User.Name,
                DateDeNaissance = User.DateBirth,
                AvatarActuel = User.Avatar
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
    
    private bool InputIsEmpty(string intput) => string.IsNullOrWhiteSpace(intput);
}