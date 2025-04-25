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
    var allResources = await missionRepository.GetAllResources();
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

        // Générer 1-3 ressources aléatoires pour ce membre
        List<Resource> memberResources = new List<Resource>();

        for (var i = 0; i < random.Next(1, 4); i++)
        {
            var resource = allResources.OrderBy(x => random.Next()).First();
            var quantity = random.Next(1, 4);
            
            var memberResource = new Resource
            {
                Id = resource.Id,
                Name = resource.Name,
                IconUrl = resource.IconUrl,
                Quantity = quantity
            };
            
            memberResources.Add(memberResource);
        }
        
        
        foreach (var bonus in activeTeamBonuses)
        {
            bonus.ApplyToResources(memberResources);
        }

        // Ajouter les ressources à la récompense et à la base de données
        foreach (var resource in memberResources)
        {
            colonReward.Resources.Add(new ResourceReward
            {
                ResourceName = resource.Name,
                Icon = resource.IconUrl,
                Quantity = resource.Quantity
            });
            
            await colonRepository.AddResourceToColonAsync(member.Id, resource.Id, resource.Quantity);
        }
        
        RewardModel.ColonRewards.Add(colonReward);
    }
}
}