using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains;

namespace StarColonies.Web.Pages;

public class UpdateProfilViewModel
{
    public string Courriel { get; set; }
    public string NomDeColon { get; set; }
    public DateTime DateDeNaissance { get; set; }
    public string NouveauMotDePasse { get; set; }
    public string ConfirmationMotDePasse { get; set; }
}

public class Profil : PageModel
{
    private readonly IColonRepository _repository;
    private readonly ILogger<ConsultMission> _logger;

    public Colon User { get; private set; }

    [BindProperty]
    public UpdateProfilViewModel UpdateProfil { get; set; }
    
    public Profil(IColonRepository colonRepository, ILogger<ConsultMission> logger)
    {
        _repository = colonRepository;
        _logger = logger;
    }


    public async Task<IActionResult> OnGet()
    {
        try
        {
            User = await _repository.GetColonByIdAsync("testUser");
        } 
        catch (Exception ex)
        {
            _logger.LogError(ex, "Profile");
        }
        return Page();
        
    }

    public async Task<IActionResult> OnPost(string slug)
    {
        // faire vérifs
        if (InputIsEmpty(UpdateProfil.NouveauMotDePasse) || InputIsEmpty(UpdateProfil.ConfirmationMotDePasse) ||
            InputIsEmpty(UpdateProfil.Courriel) || InputIsEmpty(UpdateProfil.NomDeColon))
        {
            throw new Exception("Les champs doivent etre remplis");
        }

        if (UpdateProfil.NouveauMotDePasse != UpdateProfil.ConfirmationMotDePasse)
        {
            throw new Exception("Le mot de passe ne convient pas");
        }

        try
        {
            var colonUpdate = await _repository.GetColonByIdAsync("testUser");
            colonUpdate.Name = UpdateProfil.NomDeColon;
            colonUpdate.Email = UpdateProfil.Courriel;
            colonUpdate.DateBirth = UpdateProfil.DateDeNaissance;
            colonUpdate.Password = UpdateProfil.NouveauMotDePasse;

            await _repository.UpdateColonAsync(colonUpdate);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Profile");
        }
        return Page();
    }

    private bool InputIsEmpty(string intput)
    {
        return string.IsNullOrWhiteSpace(intput);
    }
}