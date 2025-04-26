using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains;
using StarColonies.Domains.Repositories;

namespace StarColonies.Web.Pages.Admin;

[Authorize(Roles = "Admin")]
public class Users(IColonRepository colonRepository) : PageModel
{
    public IReadOnlyList<Colon> UsersList { get; set; } = new List<Colon>();

    public async Task OnGetAsync()
    {
        UsersList = await colonRepository.GetAllColonsAsync();
    }

    public async Task<IActionResult> OnPostDeleteUserAsync(string colonId)
    {
        if (string.IsNullOrEmpty(colonId))
        {
            ModelState.AddModelError(string.Empty, "User ID invalide.");
            await OnGetAsync();
            return Page();
        }

        await colonRepository.DeleteColonAsync(colonId);
        return RedirectToPage("/Admin/Users");
    }
}