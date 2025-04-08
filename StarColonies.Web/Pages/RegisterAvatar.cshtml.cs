using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StarColonies.Web.Pages;

public class RegisterAvatar : PageModel
{
    [BindProperty]
    public RegisterInputModel RegisterInput { get; set; } = new();
    public void OnGet()
    {
        
    }
}

public class RegisterAvatarInputModel
{
    public string AvatarUrl { get; set; } = string.Empty;
}