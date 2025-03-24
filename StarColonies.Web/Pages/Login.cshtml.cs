using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StarColonies.Web.Pages;


public class LogInInput() // data
{
    [Required]
    public string Identifiant { get; set; } = String.Empty;
    
    [Required]
    public string Password { get; set; } = String.Empty;
}

public class Login : PageModel
{
    private readonly ILogger<Login> _logger;
    
    [BindProperty]
    public LogInInput Input { get; set; }


    public Login(ILogger<Login> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        
    }

    public void OnPost()
    {
        /*if (!ModelState.IsValid)
        {
            return Page();
        }*/

        if (Input.Identifiant != null && Input.Password != null)
        {
            _logger.LogInformation("Test de connexion effectuée");
            TempData["identifiant"] = Input?.Identifiant;
            TempData["password"] = Input?.Password;
        }

        InputLogin();
    }

    private void InputLogin()
    {
        _logger.LogInformation($"Utilisateur : {Input.Identifiant}, Mot de passe : {Input.Password}");
    }
}