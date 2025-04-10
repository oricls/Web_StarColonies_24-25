namespace StarColonies.Infrastructures.Entities;

public class Admin
{
    public int Id { get; set; }
    public string Name {get; set;} = string.Empty;
    public string Email {get; set;} = string.Empty;
    public string Password {get; set;} = string.Empty;
    
    public ActivityLog ActivityLog {get; set;} = new ActivityLog();
    public int ActivityLogId { get; set; }
}