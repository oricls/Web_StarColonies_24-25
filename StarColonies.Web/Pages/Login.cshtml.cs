using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StarColonies.Domains.Repositories;
using StarColonies.Infrastructures.Entities;
using Log = StarColonies.Domains.Log;

namespace StarColonies.Web.Pages;


public class LogInInput() // data
{
    [Required]
    public string Identifiant { get; set; } = String.Empty;
    
    [Required]
    public string Password { get; set; } = String.Empty;
    
    [Display(Name = "Se souvenir de moi")]
    public bool RememberMe { get; set; }
}

public class Login(SignInManager<Colon> signInManager, UserManager<Colon> userManager, ILogRepository logRepository, ILogger<Login> logger) : PageModel
{
    
    [BindProperty]
    public LogInInput Input { get; set; }

    public void OnGet()
    {
        
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();
        
        // Chercher d'abord par email
        // Si non trouvé, chercher par nom de colon
        // Recherche par NameColon (vous devrez implémenter cette méthode)
        var user = await userManager.FindByEmailAsync(Input.Identifiant) ?? await userManager.Users.FirstOrDefaultAsync(u => u.UserName == Input.Identifiant);

        if (user is { UserName: not null })
            // Utiliser le UserName pour la connexion (car PasswordSignInAsync nécessite le UserName)
        {
            var result = await signInManager.PasswordSignInAsync(
                user.UserName,
                Input.Password,
                Input.RememberMe,
                lockoutOnFailure: false);

            if (result.Succeeded)
            {
                logger.LogInformation("Utilisateur connecté: {UserName}", user.UserName);
                await logRepository.AddLog(
                    new Log()
                    {
                        DateHeureAction = DateTime.Now,
                        RequeteAction = "Connexion",
                        ResponseAction = $"Success : {user.UserName} s'est connecté avec succès",
                    }
                );
                return RedirectToPage("/Dashboard");
            }
        }

        ModelState.AddModelError(string.Empty, "Identifiant ou mot de passe incorrect");
        logger.LogWarning("Tentative de connexion échouée pour l'identifiant: {Identifiant}", Input.Identifiant);
        
        await logRepository.AddLog(
            new Log()
            {
                DateHeureAction = DateTime.Now,
                RequeteAction = "Connexion",
                ResponseAction = $"Échec : {Input.Identifiant} a échoué à se connecter",
            }
        );

        return Page();
    }

    private void InputLogin()
    {
        logger.LogInformation($"Utilisateur : {Input.Identifiant}, Mot de passe : {Input.Password}");
    }
}