using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains;
using StarColonies.Domains.Repositories;

namespace StarColonies.Web.Pages.Admin;

public class Resources(IMissionRepository missionRepository) : PageModel
{
    public async Task OnGetAsync()
    {
        AvailableResources = await missionRepository.GetAllResourcesAsync();
    }
    
    public async Task<IActionResult> OnPostDeleteResourceAsync(int resourceId)
    {
        if (resourceId <= 0)
        {
            ModelState.AddModelError(string.Empty, "Resource ID invalide.");
            await OnGetAsync();
            return Page();
        }

        var resource = new Resource { Id = resourceId };
        await missionRepository.DeleteResourceAsync(resource);
        return RedirectToPage("/Admin/Resources");
    }
    
    

    public IReadOnlyCollection<Resource> AvailableResources { get; set; } = new List<Resource>();
}