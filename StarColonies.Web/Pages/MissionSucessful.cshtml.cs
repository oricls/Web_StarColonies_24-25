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

        // 4. Récupérer les membres et leurs récompenses
        var members = await teamRepository.GetMembersOfTeam(team);
        var allResources = await missionRepository.GetAllResources();
        var random = new Random();
        
        teamRepository.LevelUpTeam(team);
        
        foreach (var member in members)
        {
            var colonReward = new ColonReward
            {
                ColonName = member.Name,
                LevelGained = true // Puisqu'on a fait levelUpTeam
            };

            // Ajouter 1-3 ressources aléatoires
            for (var i = 0; i < random.Next(1, 4); i++)
            {
                var resource = allResources.OrderBy(x => random.Next()).First();
                var quantity = random.Next(1, 4);
                colonReward.Resources.Add(new ResourceReward
                {
                    ResourceName = resource.Name,
                    Icon = resource.IconUrl,
                    Quantity = quantity
                });
                await colonRepository.AddResourceToColonAsync(member.Id, resource.Id, quantity);
            }
            
            RewardModel.ColonRewards.Add(colonReward);
        }
    }
}