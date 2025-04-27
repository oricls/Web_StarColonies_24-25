using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains;
using StarColonies.Domains.Repositories;
using Microsoft.AspNetCore.Identity;
using Colon = StarColonies.Infrastructures.Entities.Colon;

namespace StarColonies.Web.Pages;

public class MissionSimulation : PageModel
{
    private readonly IMissionRepository _missionRepository;
    private readonly ITeamRepository _teamRepository;
    private readonly IColonRepository _colonRepository;
    private readonly UserManager<Colon> _userManager;
    private readonly ILogger<MissionSimulation> _logger;

    public Mission Mission { get; private set; } = new();
    public Team Team { get; private set; } = new();
    public IReadOnlyList<Bestiaire> Bestiaires { get; private set; } = new List<Bestiaire>();
    public IReadOnlyList<Domains.Colon> TeamMembers { get; private set; } = new List<Domains.Colon>();
    public ResultatMission MissionResult { get; private set; } = new();

    // Dictionnaire pour les noms de professions (cache)
    private Dictionary<int, string> _professionNamesCache = new();

    public MissionSimulation(
        IMissionRepository missionRepository,
        ITeamRepository teamRepository,
        IColonRepository colonRepository,
        UserManager<Colon> userManager,
        ILogger<MissionSimulation> logger)
    {
        _missionRepository = missionRepository;
        _teamRepository = teamRepository;
        _colonRepository = colonRepository;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<IActionResult> OnGetAsync(string slug, int teamId, int resultId)
    {
        try
        {
            // Récupérer le résultat de la mission (déjà exécutée)
            MissionResult = await _missionRepository.GetMissionResultById(resultId);

            // Récupérer la mission
            var allMissions = await _missionRepository.GetAllMissionsAsync();
            Mission = allMissions.FirstOrDefault(m => m.Name.ToKebab() == slug);

            if (Mission == null)
            {
                return NotFound(slug);
            }

            // Vérifier que l'ID de mission correspond à celui du résultat
            if (Mission.Id != MissionResult.MissionId)
            {
                return BadRequest("Incohérence entre la mission et le résultat");
            }

            // Récupérer l'équipe
            Team = await _teamRepository.GetTeamById(teamId);

            // Vérifier que l'ID d'équipe correspond à celui du résultat
            if (Team.Id != MissionResult.TeamId)
            {
                return BadRequest("Incohérence entre l'équipe et le résultat");
            }

            // Vérifier que l'utilisateur est bien membre de cette équipe
            var userId = _userManager.GetUserId(User);
            var isMember = await _teamRepository.IsUserMemberOfTeam(userId, teamId);
            if (!isMember)
            {
                return Forbid();
            }

            // Récupérer les bestiaires de la mission pour l'affichage
            Bestiaires = await _missionRepository.GetBestiairesByMissionIdAsync(Mission.Id);

            // Pré-charger les professions pour tous les membres
            await PreloadProfessionsAsync();

            // Récupérer les membres de l'équipe
            var rawTeamMembers = await _teamRepository.GetMembersOfTeam(Team);

            // Mapper les membres avec les informations complètes, y compris la profession
            TeamMembers = rawTeamMembers.Select(m => new Domains.Colon
            {
                Id = m.Id,
                Name = m.Name,
                Avatar = m.Avatar,
                Strength = m.Strength,
                Endurance = m.Endurance,
                Level = m.Level > 0 ? m.Level : 1,
                ProfessionName = m.ProfessionName
            }).ToList();
            
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "MissionSimulation.OnGet {Slug} {TeamId} {ResultId}", slug, teamId, resultId);
            return RedirectToPage("/Error");
        }
    }

    private async Task PreloadProfessionsAsync()
    {
        try
        {
            var professions = await _colonRepository.GetAllProfessionsAsync();

            foreach (var profession in professions)
            {
                _professionNamesCache[profession.Id] = profession.Name;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors du préchargement des professions");
        }
    }
}