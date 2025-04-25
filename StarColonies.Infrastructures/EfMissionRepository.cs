using Microsoft.EntityFrameworkCore;
using StarColonies.Domains;
using StarColonies.Domains.Repositories;

namespace StarColonies.Infrastructures;

public class EfMissionRepository : IMissionRepository
{
    private readonly StarColoniesContext _context;

    public EfMissionRepository(StarColoniesContext context)
    {
        _context = context;
    }

    public IReadOnlyList<Mission> GetAllMissions()
    {
        var missions = _context.Mission
            .Include(m => m.MissionBestiaires)
                .ThenInclude(mb => mb.Bestiaire)
            .ToList();

        return missions.Select(m => MapMissionEntityToDomain(m)).ToList();
    }

    public async Task<IReadOnlyList<Mission>> GetAllMissionsAsync()
    {
        var missions = await _context.Mission
            .Include(m => m.MissionBestiaires)
                .ThenInclude(mb => mb.Bestiaire)
            .ToListAsync();

        return missions.Select(m => MapMissionEntityToDomain(m)).ToList();
    }

    public Mission GetMissionById(int missionId)
    {
        var missionEntity = _context.Mission
            .Include(m => m.MissionBestiaires)
                .ThenInclude(mb => mb.Bestiaire)
                    .ThenInclude(b => b.TypeBestiaire)
            .SingleOrDefault(m => m.Id == missionId);

        if (missionEntity == null)
            return null;

        return MapMissionEntityToDomain(missionEntity);
    }

    public async Task<Mission> GetMissionByIdAsync(int missionId)
    {
        var missionEntity = await _context.Mission
            .Include(m => m.MissionBestiaires)
                .ThenInclude(mb => mb.Bestiaire)
                    .ThenInclude(b => b.TypeBestiaire)
            .SingleOrDefaultAsync(m => m.Id == missionId);

        if (missionEntity == null)
            return null;

        return MapMissionEntityToDomain(missionEntity);
    }

    public IReadOnlyList<Mission> GetMissionsByTeamId(int teamId)
    {
        var resultats = _context.ResultatMission
            .Where(rm => rm.IdTeam == teamId)
            .Include(rm => rm.Mission)
                .ThenInclude(m => m.MissionBestiaires)
                    .ThenInclude(mb => mb.Bestiaire)
            .Select(rm => rm.Mission)
            .Distinct()
            .ToList();

        return resultats.Select(m => MapMissionEntityToDomain(m)).ToList();
    }

    public async Task<IReadOnlyList<Mission>> GetMissionsByTeamIdAsync(int teamId)
    {
        var resultats = await _context.ResultatMission
            .Where(rm => rm.IdTeam == teamId)
            .Include(rm => rm.Mission)
                .ThenInclude(m => m.MissionBestiaires)
                    .ThenInclude(mb => mb.Bestiaire)
            .Select(rm => rm.Mission)
            .Distinct()
            .ToListAsync();

        return resultats.Select(m => MapMissionEntityToDomain(m)).ToList();
    }

    public IReadOnlyList<Bestiaire> GetBestiairesByMissionId(int missionId)
    {
        return _context.MissionBestiaire
            .Where(mb => mb.IdMission == missionId)
            .Include(mb => mb.Bestiaire)
            .ThenInclude(b => b.TypeBestiaire)
            .Select(mb => new Bestiaire
            {
                Id = mb.Bestiaire.Id,
                Name = mb.Bestiaire.Name,
                Strength = mb.Bestiaire.Strength,
                Endurance = mb.Bestiaire.Endurance,
                TypeBestiaireName = mb.Bestiaire.TypeBestiaire.Name,
                TypeBestiaireAvatar = mb.Bestiaire.TypeBestiaire.Avatar,
                TypeBestiaireDescription = mb.Bestiaire.TypeBestiaire.Description
            })
            .ToList();
    }

    public async Task<IReadOnlyList<Bestiaire>> GetBestiairesByMissionIdAsync(int missionId)
    {
        return await _context.MissionBestiaire
            .Where(mb => mb.IdMission == missionId)
            .Include(mb => mb.Bestiaire)
            .ThenInclude(b => b.TypeBestiaire)
            .Select(mb => new Bestiaire
            {
                Id = mb.Bestiaire.Id,
                Name = mb.Bestiaire.Name,
                Strength = mb.Bestiaire.Strength,
                Endurance = mb.Bestiaire.Endurance,
                TypeBestiaireName = mb.Bestiaire.TypeBestiaire.Name,
                TypeBestiaireAvatar = mb.Bestiaire.TypeBestiaire.Avatar,
                TypeBestiaireDescription = mb.Bestiaire.TypeBestiaire.Description
            })
            .ToListAsync();
    }

    public IReadOnlyList<ResultatMission> GetResultatsByMissionId(int missionId)
    {
        return _context.ResultatMission
            .Where(rm => rm.IdMission == missionId)
            .Include(rm => rm.Team)
            .Select(rm => new ResultatMission
            {
                Id = rm.Id,
                IssueStrength = rm.IssueStrength,
                IssueEndurance = rm.IssueEndurance,
                Date = rm.Date,
                MissionId = rm.IdMission,
                TeamId = rm.IdTeam,
                TeamName = rm.Team.Name,
                TeamLogo = rm.Team.Logo
            })
            .ToList();
    }

    public async Task<IReadOnlyList<ResultatMission>> GetResultatsByMissionIdAsync(int missionId)
    {
        return await _context.ResultatMission
            .Where(rm => rm.IdMission == missionId)
            .Include(rm => rm.Team)
            .Select(rm => new ResultatMission
            {
                Id = rm.Id,
                IssueStrength = rm.IssueStrength,
                IssueEndurance = rm.IssueEndurance,
                Date = rm.Date,
                MissionId = rm.IdMission,
                TeamId = rm.IdTeam,
                TeamName = rm.Team.Name,
                TeamLogo = rm.Team.Logo
            })
            .ToListAsync();
    }

    public void SaveOrUpdateMission(Mission mission)
    {
        var missionEntity = _context.Mission
            .Include(m => m.MissionBestiaires)
            .FirstOrDefault(m => m.Id == mission.Id);

        if (missionEntity == null)
        {
            // Nouvelle mission
            missionEntity = new Entities.Mission
            {
                Name = mission.Name,
                Image = mission.Image,
                Description = mission.Description
            };
            _context.Mission.Add(missionEntity);
        }
        else
        {
            // Mise à jour d'une mission existante
            missionEntity.Name = mission.Name;
            missionEntity.Image = mission.Image;
            missionEntity.Description = mission.Description;
        }

        _context.SaveChanges();
    }

    public async Task SaveOrUpdateMissionAsync(Mission mission)
    {
        var missionEntity = await _context.Mission
            .Include(m => m.MissionBestiaires)
            .FirstOrDefaultAsync(m => m.Id == mission.Id);

        if (missionEntity == null)
        {
            // Nouvelle mission
            missionEntity = new Entities.Mission
            {
                Name = mission.Name,
                Image = mission.Image,
                Description = mission.Description
            };
            await _context.Mission.AddAsync(missionEntity);
        }
        else
        {
            // Mise à jour d'une mission existante
            missionEntity.Name = mission.Name;
            missionEntity.Image = mission.Image;
            missionEntity.Description = mission.Description;
        }

        await _context.SaveChangesAsync();
    }

    public void SaveMissionResult(ResultatMission missionResult)
    {
        var missionResultEntity = new Entities.ResultatMission
        {
            IssueStrength = missionResult.IssueStrength,
            IssueEndurance = missionResult.IssueEndurance,
            Date = missionResult.Date,
            IdMission = missionResult.MissionId,
            IdTeam = missionResult.TeamId
        };

        _context.ResultatMission.Add(missionResultEntity);
        _context.SaveChanges();
    }

    public async Task<IReadOnlyList<Resource>> GetAllResources()
    {
        var resourceEntities = await _context.Resource
            .Include(r => r.TypeResource) // Inclure le type de ressource
            .ToListAsync();
        return resourceEntities.Select(resourceEntity => new Resource
        {
            Id = resourceEntity.Id,
            Name = resourceEntity.Name,
            Description = resourceEntity.Description,
            IconUrl = resourceEntity.TypeResource.Icon,
            TypeName = resourceEntity.TypeResource.Name, // Ajouter le nom du type
        }).ToList();
    }

    public async Task<IReadOnlyList<Bestiaire>> GetAllBestiaires()
    {
        var bestiaireEntities = await _context.Bestiaire
            .Include(b => b.TypeBestiaire)
            .ToListAsync();

        return bestiaireEntities.Select(bestiaireEntity => new Bestiaire
        {
            Id = bestiaireEntity.Id,
            Name = bestiaireEntity.Name,
            Strength = bestiaireEntity.Strength,
            Endurance = bestiaireEntity.Endurance,
            TypeBestiaireName = bestiaireEntity.TypeBestiaire.Name,
            TypeBestiaireAvatar = bestiaireEntity.TypeBestiaire.Avatar,
            TypeBestiaireDescription = bestiaireEntity.TypeBestiaire.Description
        }).ToList();
    }

    public Task AddMission(Mission mission)
    {
        var missionEntity = new Entities.Mission
        {
            Name = mission.Name,
            Image = mission.Image,
            Description = mission.Description,
            MissionBestiaires = mission.Bestiaires.Select(b => new Entities.MissionBestiaire
            {
                IdMission = mission.Id,
                IdBestiaire = b.Id
            }).ToList()
        };

        _context.Mission.Add(missionEntity);
        return _context.SaveChangesAsync();
    }

    // Méthode utilitaire pour mapper une entité Mission vers un objet de domaine Mission
    private Mission MapMissionEntityToDomain(Entities.Mission missionEntity)
    {
        int totalStrength = missionEntity.MissionBestiaires.Sum(mb => mb.Bestiaire?.Strength ?? 0);
        int totalEndurance = missionEntity.MissionBestiaires.Sum(mb => mb.Bestiaire?.Endurance ?? 0);
        
        return new Mission
        {
            Id = missionEntity.Id,
            Name = missionEntity.Name,
            Image = missionEntity.Image,
            Description = missionEntity.Description,
            BestiaireCount = missionEntity.MissionBestiaires.Count,
            TotalEndurance = totalEndurance,
            TotalStrength = totalStrength,
            Level = totalStrength
        };
    }
}