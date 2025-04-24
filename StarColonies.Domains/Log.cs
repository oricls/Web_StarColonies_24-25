namespace StarColonies.Domains;

public class Log
{
    public int Id { get; set; }

    public string RequeteAction { get; set; } = string.Empty;
    
    public string ResponseAction { get; set; } = string.Empty;
    
    public DateTime DateHeureAction { get; set; }
}