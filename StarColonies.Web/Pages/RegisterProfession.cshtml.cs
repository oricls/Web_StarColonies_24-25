using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StarColonies.Web.Pages;

public class RegisterProfession : PageModel
{
    [BindProperty]
    public RegisterProfessionInputModel RegisterProfessionInputModel { get; set; } = new();
    
    public void OnGet()
    {
        // Valeurs par défaut
        RegisterProfessionInputModel.Force = 2;
        RegisterProfessionInputModel.Endurance = 5;
        RegisterProfessionInputModel.SelectedProfession = "ingenieur"; // Profession par défaut
    }
    
    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        // Validation des points de statistiques
        if (RegisterProfessionInputModel.Force + RegisterProfessionInputModel.Endurance != 7)
        {
            ModelState.AddModelError("", "La somme des points de Force et d'Endurance doit être égale à 7.");
            return Page();
        }
        
        // Validation de la profession
        if (string.IsNullOrEmpty(RegisterProfessionInputModel.SelectedProfession))
        {
            ModelState.AddModelError("RegisterProfessionInputModel.SelectedProfession", "Veuillez sélectionner une profession.");
            return Page();
        }
        
        return RedirectToPage("/Index");
    }
}

public class RegisterProfessionInputModel
{
    [Required(ErrorMessage = "Veuillez sélectionner une profession")]
    [Display(Name = "Profession")]
    public string SelectedProfession { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "La force est requise")]
    [Range(1, 6, ErrorMessage = "La force doit être comprise entre 1 et 6")]
    [Display(Name = "Force")]
    public int Force { get; set; }
    
    [Required(ErrorMessage = "L'endurance est requise")]
    [Range(1, 6, ErrorMessage = "L'endurance doit être comprise entre 1 et 6")]
    [Display(Name = "Endurance")]
    public int Endurance { get; set; }
}