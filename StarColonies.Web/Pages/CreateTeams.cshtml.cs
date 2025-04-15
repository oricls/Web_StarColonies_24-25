using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains;
using StarColonies.Infrastructures;

namespace StarColonies.Web.Pages;

public class CreateTeams : PageModel
{
    private readonly ILogger<CreateTeams> _logger;
    private readonly ITeamRepository _teamRepository;

    public CreateTeams(ITeamRepository repository, ILogger<CreateTeams> logger)
    {
        _logger = logger;
        _teamRepository = repository;
    }
    

    [BindProperty]
    public string TeamName { get; set; } = string.Empty;
    
    public IActionResult OnPost()
    {
        // C'était pour vérifier si les requetes focntionnent bien
        var team = new Team
        {
            Name = TeamName,
            Logo = "",
        };
        _teamRepository.CreateTeamAsync(team);
        

        var teamVerif = _teamRepository.GetTeamById(team.Id);
        _logger.LogInformation($"Team {teamVerif.Id}");

        
        _teamRepository.DeleteTeamAsync(team);
        _logger.LogInformation($"Team est supprimée");
        
        return Page();
    }

}