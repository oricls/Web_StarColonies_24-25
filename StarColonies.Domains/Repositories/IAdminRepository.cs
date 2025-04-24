namespace StarColonies.Domains.Repositories;

public interface IAdminRepository
{
    Task<IList<Log>> GetLogs();
}