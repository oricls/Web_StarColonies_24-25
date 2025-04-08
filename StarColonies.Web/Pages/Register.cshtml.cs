using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StarColonies.Web.Pages;

public class Register : PageModel
{
    
    [BindProperty]
    public RegisterInputModel RegisterInput { get; set; } = new();
    
    public void OnGet()
    {
        
    }
}

public class RegisterInputModel
{
    [Required]
    public string Courriel { get; set; } = string.Empty;
    [Required]
    public string NameOfColon { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
    [Required]
    public string ConfirmPassword { get; set; } = string.Empty;
    [Required]
    public string BirthDate { get; set; } = string.Empty;
}