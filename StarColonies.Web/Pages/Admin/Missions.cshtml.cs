using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains;
using StarColonies.Domains.Repositories;

namespace StarColonies.Web.Pages.Admin;

public class Missions(IMissionRepository missionRepository) : PageModel
{
    public IReadOnlyCollection<Mission> AvailableMissions { get; set; } = new List<Mission>();

    public async Task OnGetAsync()
    {
        AvailableMissions = await missionRepository.GetAllMissionsAsync();
    }
    
    public async Task<IActionResult> OnPostDeleteMissionAsync(int missionId)
    {
        if (missionId <= 0) // missionId doit être positif
        {
            ModelState.AddModelError(string.Empty, "Mission ID invalide.");
            await OnGetAsync(); // Reload missions pour réafficher la page
            return Page();
        }

        var mission = new Mission { Id = missionId };
        await missionRepository.DeleteMissionAsync(mission);
        return RedirectToPage("/Admin/Missions");
    }
}