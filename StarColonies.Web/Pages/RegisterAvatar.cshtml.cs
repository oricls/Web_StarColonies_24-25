using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StarColonies.Web.Pages;

public class RegisterAvatarModel : PageModel
{
    [BindProperty]
    public RegisterAvatarInputModel RegisterAvatarInput { get; set; } = new();
    
    public void OnGet()
    {
    }
    
    public IActionResult OnPost()
    {
        // Validation personnalisée: soit un avatar standard, soit un avatar personnalisé
        if (string.IsNullOrEmpty(RegisterAvatarInput.SelectedAvatarId) && RegisterAvatarInput.CustomAvatar == null)
        {
            ModelState.AddModelError("", "Veuillez sélectionner un avatar ou télécharger une image personnalisée.");
            return Page();
        }
        
        // Si le ModelState n'est pas valide pour d'autres raisons
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        //TODO : Principe de transaction des données
        //TODO : Avec un post ou tempdata ?
        
        return RedirectToPage("/RegisterProfession");
    }
}

public class RegisterAvatarInputModel
{
    // Pour la sélection d'un avatar par défaut
    public string? SelectedAvatarId { get; set; }
    
    // Pour le téléchargement d'un avatar personnalisé
    [Display(Name = "Avatar personnalisé")]
    public IFormFile? CustomAvatar { get; set; }
}