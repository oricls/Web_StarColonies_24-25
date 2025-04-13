using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bestiaire = StarColonies.Domains.Bestiaire;
using Mission = StarColonies.Domains.Mission;

namespace StarColonies.Web.Pages;

public class ConsultMission : PageModel
{
    private readonly IMissionRepository _missionRepository;
    private readonly ILogger<ConsultMission> _logger;
    
    public Mission Mission { get; private set; }
    public IReadOnlyList<Bestiaire> Bestiaires { get; private set; }
    
    public ConsultMission(
        IMissionRepository missionRepository,
        ILogger<ConsultMission> logger)
    {
        _missionRepository = missionRepository;
        _logger = logger;
    }
    
    public async Task<IActionResult> OnGetAsync(string slug)
    {
        try
        {
            // Récupérer toutes les missions
            var allMissions = await _missionRepository.GetAllMissionsAsync();
            
            // Filtrer par slug (convertir le nom en slug et comparer)
            Mission = allMissions.FirstOrDefault(m => m.Name.ToKebab() == slug);
            
            if (Mission == null)
            {
                return NotFound(slug);
            }
            
            // Récupérer les bestiaires pour cette mission
            Bestiaires = await _missionRepository.GetBestiairesByMissionIdAsync(Mission.Id);
            
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ConsultMission.OnGet {Slug}", slug);
            return NotFound(slug);
        }
    }
}