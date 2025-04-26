using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains;
using StarColonies.Domains.Repositories;
using StarColonies.Web.ViewModels;

namespace StarColonies.Web.Pages;

public class MissionSucessful(
    ITeamRepository teamRepository,
    IColonRepository colonRepository,
    IMissionRepository missionRepository)
    : PageModel
{
    public MissionRewardViewModel RewardModel { get; set; } = new();

    public int Levels = 0;

    public async Task OnGetAsync(int idMission, int teamId)
{
    // 1. Récupérer l'équipe qui a réussi la mission
    var team = await teamRepository.GetTeamById(teamId);
    var mission = await missionRepository.GetMissionByIdAsync(idMission);

    // 2. Créer le modèle de récompenses
    RewardModel = new MissionRewardViewModel
    {
        MissionName = mission.Name
    };
    
    // 3. Augmenter le niveau des membres de l'équipe
    teamRepository.LevelUpTeam(team);
    Levels++;

    // 4. Récupérer les membres et leurs récompenses
    var members = await teamRepository.GetMembersOfTeam(team);
    var resourcesByMission = await missionRepository.GetResourcesByMissionIdAsync(mission.Id);
    var random = new Random();
    
    // 5. Récupérer tous les bonus actifs des membres
    List<Bonus> activeTeamBonuses = new List<Bonus>();
    bool hasExperienceBoost = false;
    
    foreach (var member in members)
    {
        var memberBonuses = await colonRepository.GetColonActiveBonusesAsync(member.Id);
        activeTeamBonuses.AddRange(memberBonuses);
        
        if (memberBonuses.Any(b => b.ApplyExperienceBonus()))
        {
            hasExperienceBoost = true;
        }
    }

    if (hasExperienceBoost)
    {
        teamRepository.LevelUpTeam(team);
        Levels++;
    }

    // 6. Distribuer les récompenses à chaque membre
    foreach (var member in members)
    {
        var colonReward = new ColonReward
        {
            ColonName = member.Name,
            LevelGained = true // Puisqu'on a fait levelUpTeam
        };

        foreach (var resource in resourcesByMission)
        {
            var quantity = random.Next(1, 4); // entre 1 et 3

            var resourceCopy = new Resource
            {
                Id = resource.Id,
                Name = resource.Name,
                IconUrl = resource.IconUrl,
                Quantity = quantity
            };

            // Appliquer les bonus sur la ressource
            foreach (var bonus in activeTeamBonuses)
            {
                bonus.ApplyToResources(new List<Resource> { resourceCopy });
            }

            colonReward.Resources.Add(new ResourceReward
            {
                ResourceName = resourceCopy.Name,
                Icon = resourceCopy.IconUrl,
                Quantity = resourceCopy.Quantity
            });

            // Enregistrer dans la BDD
            await colonRepository.AddResourceToColonAsync(member.Id, resourceCopy.Id, resourceCopy.Quantity);
        }
        
        RewardModel.ColonRewards.Add(colonReward);
    }
}
}