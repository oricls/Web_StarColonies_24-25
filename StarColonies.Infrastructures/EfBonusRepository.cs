using Microsoft.EntityFrameworkCore;
using StarColonies.Domains;
using StarColonies.Infrastructures.Entities;
using Bonus = StarColonies.Domains.Bonus;
using BonusResource = StarColonies.Domains.BonusResource;

namespace StarColonies.Infrastructures;

public class EfBonusRepository : IBonusRepository
{
    private readonly StarColoniesContext _context;
    
    public EfBonusRepository(StarColoniesContext context)
    {
        _context = context;
    }
    
    public async Task<int> CreateBonusAsync(Bonus bonus)
    {
        var bonusEntity = new Entities.Bonus
        {
            Name = bonus.Name,
            Description = bonus.Description,
            DureeParDefaut = (bonus.DateExpiration - bonus.DateAchat).Duration(),
            IconUrl = bonus.IconUrl,
            EffectTypeId = (int)bonus.EffectType, 
        
            BonusResources = bonus.Resources.Select(r => new Entities.BonusResource {
                ResourceId = r.ResourceId,
                Quantite = (int)r.Multiplier
            }).ToList()
        };
        _context.Bonus.Add(bonusEntity);
        await _context.SaveChangesAsync();
    
        // Retourner l'ID du bonus créé
        return bonusEntity.Id;
    }

    public async Task DeleteBonusAsync(Bonus bonus)
    {
        var bonusEntity = await _context.Bonus.FirstOrDefaultAsync(b => b.Id == bonus.Id);
        if (bonusEntity == null)
        {
            throw new NullReferenceException("Bonus inexistant");
        }

        _context.Bonus.Remove(bonusEntity);
        await _context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<Bonus>> GetAllBonusAsync()
    {
        var allBonusEntities = await _context.Bonus
            .Include(b => b.BonusResources)
            .ThenInclude(br => br.Resource)
            .ThenInclude(r => r.TypeResource)
            .ToListAsync();
            
        return allBonusEntities.Select(b => MapBonusEntityToDomain(b)).ToList();
    }

    public async Task<Bonus> GetBonusByName(string name)
    {
        var bonus = await _context.Bonus
            .Include(b => b.BonusResources)
            .ThenInclude(br => br.Resource)
            .ThenInclude(r => r.TypeResource)
            .SingleOrDefaultAsync(b => b.Name == name);

        if (bonus == null)
        {
            throw new Exception("Aucun bonus de ce nom n'existe");
        }

        return MapBonusEntityToDomain(bonus);
    }

    public async Task<IReadOnlyList<Bonus>> GetActivesBonuses()
    {
        var now = DateTime.Now;
        var bonusEntity = await _context.Bonus
            .Include(b => b.BonusResources)
            .ThenInclude(br => br.Resource)
            .ThenInclude(r => r.TypeResource)
            .ToListAsync();

        var bonusActifs = bonusEntity.Select(be => MapBonusEntityToDomain(be)).Where(be =>  be.DateAchat <= now  && be.DateExpiration >= now);
        return bonusActifs.ToList();
    }

    public async Task<TimeSpan> getDurationOfBonus(Bonus bonus)
    {
        var bonusEntity = await _context.Bonus.SingleOrDefaultAsync(b => b.Id == bonus.Id);
        if (bonusEntity == null)
        {
            throw new NullReferenceException("Bonus inexistant");
        }

        return bonusEntity.DureeParDefaut;
    }
    
    public async Task<IReadOnlyList<BonusResource>> GetBonusResources(Bonus bonus)
    {
        var bonusEntity = await _context.Bonus
            .Include(b => b.BonusResources)
            .ThenInclude(br => br.Resource)
            .ThenInclude(r => r.TypeResource)
            .SingleOrDefaultAsync(b => b.Id == bonus.Id);

        if (bonusEntity == null)
        {
            throw new NullReferenceException("Bonus inexistant");
        }

        return bonusEntity.BonusResources.Select(br => new BonusResource
        {
            ResourceId = br.ResourceId,
            ResourceName = br.Resource.Name,
            ResourceType = br.Resource.TypeResource.Name,
            IconUrl = br.Resource.TypeResource.Icon,
            Multiplier = br.Quantite
        }).ToList();
    }

    public async Task<IReadOnlyList<TransactionInfo>> GetColonTransactionsAsync(string colonId, int? limit = null)
    {
        var query = _context.BonusTransaction
            .Where(t => t.ColonId == colonId)
            .Include(t => t.Colon)
            .Include(t => t.Bonus)
            .Include(t => t.TransactionResources)
            .ThenInclude(tr => tr.BonusResource)
            .ThenInclude(br => br.Resource)
            .ThenInclude(r => r.TypeResource)
            .OrderByDescending(t => t.TransactionDate);
                
        if (limit.HasValue)
            query = (IOrderedQueryable<BonusTransaction>)query.Take(limit.Value);
                
        var transactions = await query.ToListAsync();
            
        // Convertir les entités en domaine
        var result = new List<TransactionInfo>();
        foreach (var t in transactions)
        {
            result.Add(new TransactionInfo
            {
                Id = t.Id,
                ColonId = t.ColonId,
                ColonName = t.Colon.UserName,
                BonusId = t.BonusId,
                BonusName = t.Bonus.Name,
                BonusIconUrl = t.Bonus.IconUrl,
                TransactionDate = t.TransactionDate,
                Resources = t.TransactionResources.Select(tr => new TransactionResourceInfo
                {
                    ResourceId = tr.BonusResource.ResourceId,
                    ResourceName = tr.BonusResource.Resource.Name,
                    ResourceType = tr.BonusResource.Resource.TypeResource.Name,
                    IconUrl = tr.BonusResource.Resource.TypeResource.Icon,
                    Quantite = tr.Quantite
                }).ToList()
            });
        }
            
        return result;
    }

    public async Task CreateTransactionAsync(string colonId, int bonusId, List<BonusResource> resources)
    {
        // Créer la transaction
        var transaction = new Entities.BonusTransaction
        {
            ColonId = colonId,
            BonusId = bonusId,
            TransactionDate = DateTime.Now
        };
            
        await _context.BonusTransaction.AddAsync(transaction);
        await _context.SaveChangesAsync(); // Pour obtenir l'ID de la transaction
            
        // Ajouter les ressources utilisées
        foreach (var resource in resources)
        {
            // Récupérer le BonusResource pour avoir l'ID
            var bonusResourceEntity = await _context.BonusResource
                .FirstOrDefaultAsync(br => br.BonusId == bonusId && br.ResourceId == resource.ResourceId);
                
            if (bonusResourceEntity != null)
            {
                var transactionResource = new Entities.BonusTransactionResource
                {
                    TransactionId = transaction.Id,
                    BonusResourceId = bonusResourceEntity.Id,
                    Quantite = resource.Multiplier
                };
                    
                await _context.BonusTransactionResource.AddAsync(transactionResource);
            }
        }
            
        await _context.SaveChangesAsync();
    }
    private Bonus MapBonusEntityToDomain(Entities.Bonus bonusEntity)
    {
        var dateAchat = DateTime.Now; //Todo: a revoir ? -> pas très SOLID mais ça marche
        var dateExpiration = dateAchat.Add(bonusEntity.DureeParDefaut);
        
        return new Bonus
        {
            Id = bonusEntity.Id,
            Name = bonusEntity.Name,
            Description = bonusEntity.Description,
            DateAchat = dateAchat,
            DateExpiration = dateExpiration,
            IconUrl = bonusEntity.IconUrl,
            EffectType = (BonusEffectType)bonusEntity.EffectTypeId, // Conversion élégante !
            Resources = bonusEntity.BonusResources.Select(br => new BonusResource
            {
                ResourceId = br.ResourceId,
                ResourceName = br.Resource.Name,
                ResourceType = br.Resource.TypeResource.Name,
                IconUrl = br.Resource.TypeResource.Icon,
                Multiplier = br.Quantite
            }).ToList()
        };
    }
}