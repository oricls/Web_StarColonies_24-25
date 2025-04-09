namespace StarColonies.Infrastructures.Entities;

public class Log
{
    public int Id { get; set; }
    public string RequeteAction { get; set; } = string.Empty;
    public string ReponseAction { get; set; } = string.Empty;
    public DateTime DateHeureAction { get; set; }
    
    // clés
    public int ActivityLogId { get; set; }
    public ActivityLog ActivityLog { get; set; } = null!;
}