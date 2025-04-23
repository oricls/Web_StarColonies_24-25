using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains;
using System.Security.Claims;

namespace StarColonies.Web.Pages;

[Authorize]
public class Comptoir : PageModel
{
    private readonly ILogger<ConsultMission> _logger;
    private readonly IBonusRepository _repositoryBonus;
    private readonly IColonRepository _repositoryColon;

    public IReadOnlyCollection<Bonus> Bonuses { get; private set; }
    public Dictionary<int, TimeSpan> BonusDuration { get; private set; }
    
    // Propriété pour stocker les ressources groupées par type
    public IReadOnlyCollection<ResourceGroupViewModel> GroupedResources { get; private set; }
    
    public string StatusMessage { get; set; }
    
    public Dictionary<int, bool> ActiveBonusStatus { get; private set; }
    public Dictionary<int, string> RemainingTime { get; private set; }
    
    public Dictionary<int, DateTime> ExpirationDates { get; private set; }
    
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
            // Récupérer l'identifiant de l'utilisateur connecté
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            Bonuses = await _repositoryBonus.GetAllBonusAsync();
            var resourcesOfColon = await _repositoryColon.GetColonResourcesAsync(userId);
            
            // Récupérer les bonus actifs du colon
            var activeBonuses = await _repositoryColon.GetColonActiveBonusesAsync(userId);
            
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
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors du chargement de la page Comptoir");
        }
        return Page();
    }
    
    public async Task<IActionResult> OnPostAcheterBonusAsync(int bonusId)
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
                return RedirectToPage();
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
                    return RedirectToPage();
                }
            }
            
            // Déduire les ressources et effectuer l'achat
            foreach (var requiredResource in bonus.Resources)
            {
                // Trouver les ressources du type requis
                //TODO : Se débarasser du typeresource... 
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
            
            // Ajouter le bonus au colon
            await _repositoryColon.AddBonusToColonAsync(userId, bonusId, TimeSpan.Zero); // Utilise la durée par défaut du bonus
            
            StatusMessage = $"Le bonus {bonus.Name} a été acheté avec succès !";
            return RedirectToPage();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de l'achat d'un bonus");
            StatusMessage = "Une erreur est survenue lors de l'achat du bonus.";
            return RedirectToPage();
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