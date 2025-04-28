using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains;
using StarColonies.Domains.Repositories;

namespace StarColonies.Web.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ITeamRepository _teamRepository;
    
    public List<TeamRankingModel> TopTeams { get; set; } = new List<TeamRankingModel>();

    public IndexModel(ILogger<IndexModel> logger, ITeamRepository teamRepository)
    {
        _logger = logger;
        _teamRepository = teamRepository;
    }

    public async Task OnGetAsync()
    {
        // Récupérer les 10 meilleures équipes
        var topTeams = await _teamRepository.GetTopTeamsAsync(10);
        TopTeams = new List<TeamRankingModel>(topTeams);
    }
}