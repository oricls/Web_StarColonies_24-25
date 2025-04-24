namespace StarColonies.Domains.Repositories;

public interface ILogRepository
{
    Task<IList<Log>> GetLogs();
    
    Task<IList<Log>> GetLogsByDateDescending(DateTime date);
    
    Task AddLog(Log log);
}