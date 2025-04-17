using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains;

namespace StarColonies.Web.Pages;

public class Comptoir : PageModel
{
    private readonly ILogger<ConsultMission> _logger;
    private readonly IBonusRepository _repository;

    public IReadOnlyCollection<Bonus> Bonuses { get; private set; }
    //public IReadOnlyList<BonusResource> BonusesResources { get; private set; } -> plus utilsie si on fait une itération dans comptoir.html
    public TimeSpan BonusDuration { get; private set; }
    
    

    public Comptoir(IBonusRepository repository, ILogger<ConsultMission> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            Bonuses = await _repository.GetAllBonusAsync();
            foreach (var bonus in Bonuses)
            {
                //BonusesResources = await _repository.GetBonusResources(bonus);
                BonusDuration = await _repository.getDurationOfBonus(bonus);

            }

           
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Comptoir");
        }
        return Page();
    }
}