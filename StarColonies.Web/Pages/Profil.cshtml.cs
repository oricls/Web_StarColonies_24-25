using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains;

namespace StarColonies.Web.Pages;

public class Profil : PageModel
{
    private readonly IColonRepository _repository;
    private readonly ILogger<ConsultMission> _logger;

    public Colon User { get; private set; }

    public Profil(IColonRepository colonRepository, ILogger<ConsultMission> logger)
    {
        _repository = colonRepository;
        _logger = logger;
    }


    public async Task<IActionResult> OnGet()
    {
        try
        {
            User = await _repository.GetColonByIdAsync("testUser");
        } 
        catch (Exception ex)
        {
            _logger.LogError(ex, "Profile");
        }
        return Page();
        
    }

    public void OnPost(string slug)
    {
        
    }
}