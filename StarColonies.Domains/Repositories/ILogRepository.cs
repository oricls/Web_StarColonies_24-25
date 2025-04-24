namespace StarColonies.Domains.Repositories;

public interface ILogRepository
{
    Task<IList<Log>> GetLogs();
    
    Task<IList<Log>> GetLogsByDate(DateTime date);
    Task AddLog(Log log);
}