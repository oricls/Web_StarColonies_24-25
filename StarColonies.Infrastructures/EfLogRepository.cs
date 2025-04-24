using StarColonies.Domains;
using StarColonies.Domains.Repositories;

namespace StarColonies.Infrastructures;

public class EfLogRepository(StarColoniesContext context) : ILogRepository
{
    public Task<IList<Log>> GetLogs()
    {
        var logs = context.Logs.ToList();
        return Task.FromResult<IList<Log>>(logs.Select(l => MapLogEntityToDomain(l)).ToList());
    }

    public Task<IList<Log>> GetLogsByDateDescending(DateTime date)
    {
        var logs = context.Logs
            .Where(l => l.DateHeureAction.Date == date.Date)
            .OrderByDescending(l => l.DateHeureAction)
            .ToList();
        return Task.FromResult<IList<Log>>(logs.Select(l => MapLogEntityToDomain(l)).ToList());
    }

    public Task AddLog(Log log)
    {
        var logEntity = MapLogDomainToEntity(log);
        context.Logs.Add(logEntity);
        context.SaveChanges();
        return Task.CompletedTask;
    }

    private Entities.Log MapLogDomainToEntity(Log log)
    {
        return new Entities.Log
        {
            Id = log.Id,
            RequeteAction = log.RequeteAction,
            ResponseAction = log.ResponseAction,
            DateHeureAction = log.DateHeureAction
        };
    }

    private Log MapLogEntityToDomain(Entities.Log log)
    {
        return new Log
        {
            Id = log.Id,
            RequeteAction = log.RequeteAction,
            ResponseAction = log.ResponseAction,
            DateHeureAction = log.DateHeureAction
        };
    }
}