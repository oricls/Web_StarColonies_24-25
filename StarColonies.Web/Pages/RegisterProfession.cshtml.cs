using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StarColonies.Web.Pages;

public class RegisterProfession : PageModel
{
    [BindProperty]
    public RegisterProfessionInputModel RegisterProfessionInputModel { get; set; } = new();
    
    public void OnGet()
    {
        
    }
}

public class RegisterProfessionInputModel
{
    public string Profession { get; set; } = string.Empty;
    public int Force { get; set; }
    public int Endurance { get; set; }
}