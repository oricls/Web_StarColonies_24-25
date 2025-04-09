namespace StarColonies.Infrastructures.Entities;

public class ActivityLog
{
    public int Id { get; set; }

    public IList<Log> Logs { get; set; } = new List<Log>();
    
    public IList<Admin> Admins { get; set; } = new List<Admin>();
}