using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StarColonies.Web.Pages;

public class RegisterModel : PageModel
{
    [BindProperty]
    public RegisterInputModel RegisterInput { get; set; } = new();
    
    public void OnGet()
    {
    }
    
    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        //TODO :Appelle au repository ?
        //TODO : Partager les données au suivant ?
        
        return RedirectToPage("/RegisterAvatar");
    }
}

public class RegisterInputModel
{
    // Tu sais pas dire email comme tout le monde ?
    [Required(ErrorMessage = "Le courriel est requis")]
    [EmailAddress(ErrorMessage = "Format de courriel invalide")]
    [Display(Name = "Courriel")]
    public string Courriel { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le nom de colon est requis")]
    [Display(Name = "Nom de colon")]
    public string NameOfColon { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le mot de passe est requis")]
    //A préciser
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