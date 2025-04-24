using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains;
using StarColonies.Domains.Repositories;
using System.Security.Claims;
using IColonRepository = StarColonies.Domains.IColonRepository;

namespace StarColonies.Web.Pages;

[Authorize]
public class Comptoir : PageModel
{
    private readonly ILogger<ConsultMission> _logger;
    private readonly IBonusRepository _repositoryBonus;
    private readonly IColonRepository _repositoryColon;
    private readonly IMissionRepository _repositoryMission;

    public IReadOnlyCollection<Bonus> Bonuses { get; private set; }
    public Dictionary<int, TimeSpan> BonusDuration { get; private set; }
    
    // Propriété pour stocker les ressources groupées par type
    public IReadOnlyCollection<ResourceGroupViewModel> GroupedResources { get; private set; }
    
    // Propriété pour stocker l'historique des transactions
    public IReadOnlyCollection<TransactionInfo> TransactionHistory { get; private set; }
    
    public string StatusMessage { get; set; }
    
    public Dictionary<int, bool> ActiveBonusStatus { get; private set; }
    public Dictionary<int, string> RemainingTime { get; private set; }
    
    public Dictionary<int, DateTime> ExpirationDates { get; private set; }
    
    // Nouvelles propriétés pour le suivi des achats
    [BindProperty(SupportsGet = true)]
    public string FromMission { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public int PurchaseCount { get; set; } = 0;
    
    public bool TeamBonusAvailable => PurchaseCount >= 5;
    public bool TeamBonusCreated { get; set; } = false;
    public Mission CurrentMission { get; set; }
    
    public Comptoir(
        IBonusRepository repositoryBonus, 
        IColonRepository repositoryColon, 
        IMissionRepository repositoryMission,
        ILogger<ConsultMission> logger)
    {
        _repositoryBonus = repositoryBonus;
        _repositoryColon = repositoryColon;
        _repositoryMission = repositoryMission;
        _logger = logger;
        BonusDuration = new Dictionary<int, TimeSpan>();
    }

    public async Task<IActionResult> OnGetAsync(bool bonusCreated = false)
    {
        try
        {
            // Récupérer l'identifiant de l'utilisateur connecté
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            Bonuses = await _repositoryBonus.GetAllBonusAsync();
            var resourcesOfColon = await _repositoryColon.GetColonResourcesAsync(userId);
            
            // Récupérer les bonus actifs du colon
            var activeBonuses = await _repositoryColon.GetColonActiveBonusesAsync(userId);
            
            // Récupérer l'historique des transactions du colon (limité aux 10 dernières)
            TransactionHistory = await _repositoryBonus.GetColonTransactionsAsync(userId, 10);
            
            // Grouper les ressources par type et calculer les totaux
            GroupedResources = resourcesOfColon
                .GroupBy(r => r.TypeName)
                .Select(group => new ResourceGroupViewModel
                {
                    Type = group.Key,
                    TotalQuantity = group.Sum(r => r.Quantity),
                    IconUrl = group.FirstOrDefault()?.IconUrl
                })
                .ToList();
            
            // Initialiser les dictionnaires
            BonusDuration = new Dictionary<int, TimeSpan>();
            ActiveBonusStatus = new Dictionary<int, bool>();
            RemainingTime = new Dictionary<int, string>();
            ExpirationDates = new Dictionary<int, DateTime>();
            
            // Vérifier quels bonus sont actifs et calculer le temps restant
            var now = DateTime.Now;
            foreach (var bonus in Bonuses)
            {
                BonusDuration[bonus.Id] = await _repositoryBonus.getDurationOfBonus(bonus);
                
                // Vérifier si ce bonus est actif pour le colon
                var activeBonus = activeBonuses.FirstOrDefault(b => b.Id == bonus.Id);
                ActiveBonusStatus[bonus.Id] = activeBonus != null;
                
                if (activeBonus != null)
                {
                    // Stocker la date d'expiration exacte
                    ExpirationDates[bonus.Id] = activeBonus.DateExpiration;
                    
                    var timeSpan = activeBonus.DateExpiration - now;
                    if (timeSpan.TotalDays >= 1)
                    {
                        RemainingTime[bonus.Id] = $"{(int)timeSpan.TotalDays}j {timeSpan.Hours}h";
                    }
                    else if (timeSpan.TotalHours >= 1)
                    {
                        RemainingTime[bonus.Id] = $"{timeSpan.Hours}h {timeSpan.Minutes}m";
                    }
                    else
                    {
                        RemainingTime[bonus.Id] = $"{timeSpan.Minutes}m {timeSpan.Seconds}s";
                    }
                }
            }
            
            // Si on vient d'une mission, récupérer la mission correspondante
            if (!string.IsNullOrEmpty(FromMission))
            {
                var allMissions = await _repositoryMission.GetAllMissionsAsync();
                CurrentMission = allMissions.FirstOrDefault(m => m.Name.ToKebab() == FromMission);
                
                // Si le bonus est disponible mais pas encore créé et qu'on n'a pas un indicateur qu'il a déjà été créé
                TeamBonusCreated = bonusCreated;
                if (TeamBonusAvailable && !TeamBonusCreated)
                {
                    // Créer le bonus d'équipe
                    await CreateTeamLevelBonus(userId);
                    
                    // Rediriger pour marquer le bonus comme créé
                    return RedirectToPage(new { fromMission = FromMission, purchaseCount = PurchaseCount, bonusCreated = true });
                }
                
                if (bonusCreated)
                {
                    StatusMessage = "Félicitations ! Vous avez débloqué un bonus d'augmentation de niveau pour votre équipe!";
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors du chargement de la page Comptoir");
        }
        return Page();
    }
    
    public async Task<IActionResult> OnPostAcheterBonusAsync(int bonusId, string fromMission, int currentCount)
    {
        try
        {
            // Récupérer l'identifiant de l'utilisateur connecté
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            // Récupérer le bonus à acheter
            var bonuses = await _repositoryBonus.GetAllBonusAsync();
            var bonus = bonuses.FirstOrDefault(b => b.Id == bonusId);
            
            if (bonus == null)
            {
                StatusMessage = "Erreur : Bonus non trouvé.";
                return RedirectToPage(new { fromMission, purchaseCount = currentCount });
            }
            
            // Récupérer les ressources du colon
            var resourcesOfColon = await _repositoryColon.GetColonResourcesAsync(userId);
            var groupedResources = resourcesOfColon
                .GroupBy(r => r.TypeName)
                .ToDictionary(
                    group => group.Key,
                    group => new ResourceGroupViewModel
                    {
                        Type = group.Key,
                        TotalQuantity = group.Sum(r => r.Quantity),
                        Resources = group.ToList()
                    }
                );
            
            // Vérifier si le colon a suffisamment de ressources
            foreach (var requiredResource in bonus.Resources)
            {
                if (!groupedResources.TryGetValue(requiredResource.ResourceType, out var colonResource) || 
                    colonResource.TotalQuantity < requiredResource.Multiplier)
                {
                    StatusMessage = "Erreur : Ressources insuffisantes pour effectuer cet échange.";
                    return RedirectToPage(new { fromMission, purchaseCount = currentCount });
                }
            }
            
            // Déduire les ressources et effectuer l'achat
            foreach (var requiredResource in bonus.Resources)
            {
                // Trouver les ressources du type requis 
                var resourcesOfType = groupedResources[requiredResource.ResourceType].Resources;
                
                int remainingToDeduct = requiredResource.Multiplier;
                
                foreach (var resource in resourcesOfType)
                {
                    int toDeduct = Math.Min(resource.Quantity, remainingToDeduct);
                    
                    // Mise à jour de la quantité de ressource
                    await _repositoryColon.UpdateResourceQuantityAsync(userId, resource.Id, resource.Quantity - toDeduct);
                    
                    remainingToDeduct -= toDeduct;
                    if (remainingToDeduct <= 0)
                        break;
                }
            }
            
            // Enregistrer la transaction
            await _repositoryBonus.CreateTransactionAsync(userId, bonusId, bonus.Resources.ToList());
            
            // Ajouter le bonus au colon
            await _repositoryColon.AddBonusToColonAsync(userId, bonusId, TimeSpan.Zero); // Utilise la durée par défaut du bonus
            
            // Si on est en train d'acheter des bonus pour débloquer le bonus d'équipe
            if (!string.IsNullOrEmpty(fromMission))
            {
                // Incrémenter le compteur
                currentCount++;
                
                // Vérifier si on atteint 5 achats
                if (currentCount == 5)
                {
                    StatusMessage = $"Le bonus {bonus.Name} a été acheté avec succès ! Vous avez débloqué un bonus d'augmentation de niveau pour votre équipe!";
                }
                else
                {
                    StatusMessage = $"Le bonus {bonus.Name} a été acheté avec succès ! Encore {5 - currentCount} achat(s) pour débloquer le bonus d'équipe.";
                }
            }
            else
            {
                StatusMessage = $"Le bonus {bonus.Name} a été acheté avec succès !";
            }
            
            return RedirectToPage(new { fromMission, purchaseCount = currentCount });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de l'achat d'un bonus");
            StatusMessage = "Une erreur est survenue lors de l'achat du bonus.";
            return RedirectToPage(new { fromMission, purchaseCount = currentCount });
        }
    }
    
    private async Task CreateTeamLevelBonus(string userId)
    {
        try
        {
            // Créer un bonus d'augmentation de niveau avec un nom spécial pour l'identifier
            var levelBonus = new Bonus
            {
                Name = "Boost de niveau d'équipe (usage unique)",
                Description = "Augmente le niveau de chaque membre de l'équipe de 1 pour la prochaine mission uniquement",
                EffectType = BonusEffectType.IncreaseLevel,
                IconUrl = "icons/temp.png",
                DateAchat = DateTime.Now,
                DateExpiration = DateTime.Now.AddHours(12) // Durée suffisante pour retourner à la mission
            };
            
            // Sauvegarder le bonus
            var bonusId = await _repositoryBonus.CreateBonusAsync(levelBonus);
            
            // Associer le bonus au colon
            await _repositoryColon.AddBonusToColonAsync(userId, bonusId, TimeSpan.FromHours(12));
            
            _logger.LogInformation($"Bonus d'équipe à usage unique créé pour l'utilisateur {userId} (ID: {bonusId})");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la création du bonus d'équipe");
        }
    }
}

// Classe ViewModel pour représenter les ressources groupées
public class ResourceGroupViewModel
{
    public string Type { get; set; }
    public int TotalQuantity { get; set; }
    public string IconUrl { get; set; }
    public List<Resource> Resources { get; set; } = new List<Resource>();
}