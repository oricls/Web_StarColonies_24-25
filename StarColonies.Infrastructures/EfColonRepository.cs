using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StarColonies.Domains;

namespace StarColonies.Infrastructures;

public class EfColonRepository : IColonRepository
{
    private readonly StarColoniesContext _context;
    private readonly UserManager<Entities.Colon> _userManager;

    public EfColonRepository(StarColoniesContext context, UserManager<Entities.Colon> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IReadOnlyList<Colon>> GetAllColonsAsync()
    {
        var colons = await _context.Users
            .Include(c => c.Profession)
            .ToListAsync();

        return colons.Select(c => MapColonEntityToDomain(c)).ToList();
    }

    public async Task<Colon> GetColonByIdAsync(string colonId)
    {
        var colonEntity = await _context.Users
            .Include(c => c.Profession)
            .Include(c => c.ColonResources)
                .ThenInclude(cr => cr.Resource)
            .Include(c => c.ColonBonuses)
                .ThenInclude(cb => cb.Bonus)
            .SingleOrDefaultAsync(c => c.Id == colonId);

        if (colonEntity == null)
            return null;

        return MapColonEntityToDomain(colonEntity);
    }

    public async Task<Colon> GetColonByEmailAsync(string email)
    {
        var colonEntity = await _context.Users
            .Include(c => c.Profession)
            .SingleOrDefaultAsync(c => c.Email == email);

        if (colonEntity == null)
            return null;

        return MapColonEntityToDomain(colonEntity);
    }

    public async Task CreateColonAsync(Colon colon)
    {
        var colonEntity = new Entities.Colon
        {
            UserName = colon.Name,
            Email = colon.Email,
            DateBirth = DateTime.Now,
            Endurance = 0,
            Strength = 0,
            Level = 1,
            Avatar = colon.Avatar, // Utilisation de l'avatar fourni par le domaine
            IdProfession = 1 // Valeur par défaut, à ajuster selon votre système
        };

        var result = await _userManager.CreateAsync(colonEntity, colon.Password);
        
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception($"Erreur lors de la création du colon: {errors}");
        }
    }

    public async Task UpdateColonAsync(Colon colon)
    {
        var colonEntity = await _context.Users
            .SingleOrDefaultAsync(c => c.Id == colon.Id);

        if (colonEntity == null)
            throw new KeyNotFoundException($"Colon avec ID {colon.Id} non trouvé");

        colonEntity.UserName = colon.Name;
        colonEntity.Email = colon.Email;
        colonEntity.Avatar = colon.Avatar; // Mise à jour de l'avatar
        // Ne pas mettre à jour le mot de passe ici, utiliser UserManager.ChangePasswordAsync

        await _context.SaveChangesAsync();
    }

    /**
     * TODO : Si un colon fait partie d'une team il ne sera pas supprimé de celle-ci.
     * Que se passera-t-il ? "Theory will only take you so far." J. Robert Oppenheimer
     */
    public async Task DeleteColonAsync(string colonId)
    {
        var colonEntity = await _context.Users
            .SingleOrDefaultAsync(c => c.Id == colonId);

        if (colonEntity == null)
            throw new KeyNotFoundException($"Colon avec ID {colonId} non trouvé");

        // Supprimer d'abord les ressources associées
        var colonResources = await _context.ColonResource
            .Where(cr => cr.ColonId == colonId)
            .ToListAsync();
    
        _context.ColonResource.RemoveRange(colonResources);
    
        // Supprimer les bonus associés
        var colonBonuses = await _context.ColonBonus
            .Where(cb => cb.ColonId == colonId)
            .ToListAsync();
    
        _context.ColonBonus.RemoveRange(colonBonuses);
    
        // Sauvegarder les suppressions des relations
        await _context.SaveChangesAsync();
    
        // Maintenant supprimer le colon lui-même
        _context.Users.Remove(colonEntity);
        await _context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<Resource>> GetColonResourcesAsync(string colonId)
    {
        var resources = await _context.ColonResource
            .Where(cr => cr.ColonId == colonId)
            .Include(cr => cr.Resource)
                .ThenInclude(r => r.TypeResource)
            .Select(cr => new Resource
            {
                Id = cr.Resource.Id,
                Name = cr.Resource.Name,
                Description = cr.Resource.Description,
                TypeName = cr.Resource.TypeResource.Name,
                Quantity = cr.Quantity
            })
            .ToListAsync();

        return resources;
    }

    public async Task<IReadOnlyList<Bonus>> GetColonActiveBonusesAsync(string colonId)
    {
        var now = DateTime.UtcNow;
        
        var bonuses = await _context.ColonBonus
            .Where(cb => cb.ColonId == colonId && cb.DateExpiration > now)
            .Include(cb => cb.Bonus)
                .ThenInclude(b => b.BonusResources)
                    .ThenInclude(br => br.Resource)
            .Select(cb => new Bonus
            {
                Id = cb.Bonus.Id,
                Name = cb.Bonus.Name,
                Description = cb.Bonus.Description,
                DateAchat = cb.DateAchat,
                DateExpiration = cb.DateExpiration,
                Resources = cb.Bonus.BonusResources.Select(br => new BonusResource
                {
                    ResourceId = br.ResourceId,
                    ResourceName = br.Resource.Name,
                    Multiplier = br.Quantite
                }).ToList()
            })
            .ToListAsync();

        return bonuses;
    }

    public async Task AddResourceToColonAsync(string colonId, int resourceId, int quantity)
    {
        var colonResource = await _context.ColonResource
            .SingleOrDefaultAsync(cr => cr.ColonId == colonId && cr.ResourceId == resourceId);

        if (colonResource != null)
        {
            // La ressource existe déjà, mettre à jour la quantité
            colonResource.Quantity += quantity;
        }
        else
        {
            // Nouvelle ressource pour ce colon
            colonResource = new Entities.ColonResource
            {
                ColonId = colonId,
                ResourceId = resourceId,
                Quantity = quantity
            };
            await _context.ColonResource.AddAsync(colonResource);
        }

        await _context.SaveChangesAsync();
    }

    public async Task AddBonusToColonAsync(string colonId, int bonusId, TimeSpan duration)
    {
        var bonus = await _context.Bonus
            .SingleOrDefaultAsync(b => b.Id == bonusId);

        if (bonus == null)
            throw new KeyNotFoundException($"Bonus avec ID {bonusId} non trouvé");

        // Si duration est TimeSpan.Zero, utiliser la durée par défaut du bonus
        var actualDuration = duration == TimeSpan.Zero ? bonus.DureeParDefaut : duration;
        
        var now = DateTime.UtcNow;
        var expiration = now.Add(actualDuration);

        var colonBonus = new Entities.ColonBonus
        {
            ColonId = colonId,
            BonusId = bonusId,
            DateAchat = now,
            DateExpiration = expiration
        };

        await _context.ColonBonus.AddAsync(colonBonus);
        await _context.SaveChangesAsync();
    }

    
    public async Task<IReadOnlyList<Profession>> GetAllProfessionsAsync()
    {
        var professions = await _context.Profession
            .OrderBy(p => p.Name)
            .Select(p => new Profession
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description
            })
            .ToListAsync();

        return professions;
    }

    private Colon MapColonEntityToDomain(Entities.Colon colonEntity)
    {
        return new Colon
        {
            Id = colonEntity.Id,
            Name = colonEntity.UserName,
            Email = colonEntity.Email,
            Password = string.Empty, // Ne jamais renvoyer le mot de passe, même hashé mouhahaha !
            Avatar = colonEntity.Avatar // Ajout de l'avatar
        };
    }
}