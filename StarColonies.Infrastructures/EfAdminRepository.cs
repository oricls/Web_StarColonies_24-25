using Microsoft.EntityFrameworkCore;
using StarColonies.Domains;
using StarColonies.Domains.Repositories;

namespace StarColonies.Infrastructures;

public class EfAdminRepository(StarColoniesContext context) : IAdminRepository
{
    public Task<IList<Log>> GetLogs()
    {
        var logs = context.Logs.ToList();
        return Task.FromResult<IList<Log>>(logs.Select(l => MapLogEntityToDomain(l)).ToList());
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