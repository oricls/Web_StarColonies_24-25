using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains;
using StarColonies.Domains.Repositories;

namespace StarColonies.Web.Pages;

public class Comptoir : PageModel
{
    private readonly ILogger<ConsultMission> _logger;
    private readonly IBonusRepository _repositoryBonus;
    private readonly IColonRepository _repositoryColon;

    public IReadOnlyCollection<Bonus> Bonuses { get; private set; }
    public IReadOnlyList<Resource> ResourcesOfColon { get; private set; }
    public Dictionary<int, TimeSpan> BonusDuration { get; private set; }
    
    public Comptoir(IBonusRepository repositoryBonus, IColonRepository repositoryColon, ILogger<ConsultMission> logger)
    {
        _repositoryBonus = repositoryBonus;
        _repositoryColon = repositoryColon;
        _logger = logger;
        BonusDuration = new Dictionary<int, TimeSpan>();
    }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            Bonuses = await _repositoryBonus.GetAllBonusAsync();
            ResourcesOfColon = await _repositoryColon.GetColonResourcesAsync("testUser");
            foreach (var bonus in Bonuses)
            {
                BonusDuration[bonus.Id] = await _repositoryBonus.getDurationOfBonus(bonus);
            }
            
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Comptoir");
        }
        return Page();
    }
}