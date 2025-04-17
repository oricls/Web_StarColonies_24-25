using Microsoft.EntityFrameworkCore;
using StarColonies.Domains;

namespace StarColonies.Infrastructures;

public class EfBonusRepository : IBonusRepository
{
    private readonly StarColoniesContext _context;
    
    public EfBonusRepository(StarColoniesContext context)
    {
        _context = context;
    }
    
    public async Task CreateBonusAsync(Bonus bonus)
    {
        var bonusEntity = new Entities.Bonus
        {
            Name = bonus.Name,
            Description = bonus.Description,
            DureeParDefaut = (bonus.DateExpiration - bonus.DateAchat).Duration(),
            
            BonusResources = bonus.Resources.Select(r => new Entities.BonusResource {
                ResourceId = r.ResourceId,
                Quantite = (int)r.Multiplier
            }).ToList()
        };
        _context.Bonus.Add(bonusEntity);
        await _context.SaveChangesAsync();
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
        var allBonusEntities = await _context.Bonus.Include(b => b.BonusResources).ToListAsync();
        return allBonusEntities.Select(b => MapBonusEntityToDomain(b)).ToList();
    }

    public async Task<Bonus> GetBonusByName(string name)
    {
        var bonus = await _context.Bonus.Include(b => b.BonusResources).SingleOrDefaultAsync(b => b.Name == name);

        if (bonus == null)
        {
            throw new Exception("Aucun bonus de ce nom n'existe");
        }

        return MapBonusEntityToDomain(bonus);
    }

    public async Task<IReadOnlyList<Bonus>> GetActivesBonuses()
    {
        var now = DateTime.Now;
        var bonusEntity = await _context.Bonus.Include(b => b.BonusResources).ToListAsync();

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
        var bonusEntity = await _context.Bonus.Include(b => b.BonusResources)
            .ThenInclude(br => br.Resource).SingleOrDefaultAsync(b => b.Id == bonus.Id);

        if (bonusEntity == null)
        {
            throw new NullReferenceException("Bonus inexistant");
        }

        return bonusEntity.BonusResources.Select(br => new BonusResource
        {
            ResourceId = br.ResourceId,
            ResourceName = br.Resource.Name,
            Multiplier = br.Quantite
        }).ToList();
    }

    private Bonus MapBonusEntityToDomain(Entities.Bonus bonusEntity)
    {
        var dateAchat = DateTime.Now; // a revoir ? 
        var dateExpiration = dateAchat.Add(bonusEntity.DureeParDefaut);
        
        return new Bonus
        {
            Id = bonusEntity.Id,
            Name = bonusEntity.Name,
            Description = bonusEntity.Description,
            DateAchat = dateAchat,
            DateExpiration =dateExpiration,
            Resources = bonusEntity.BonusResources.Select(br => new BonusResource
            {
                ResourceId = br.ResourceId,
                ResourceName = br.Resource.Name,
                Multiplier = br.Quantite
            }).ToList()
        };
    }
}