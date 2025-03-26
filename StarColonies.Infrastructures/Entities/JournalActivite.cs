namespace StarColonies.Infrastructures.Entities;

public class JournalActivite
{
    public int IdJournal { get; set; }
    public string RequeteAction { get; set; }
    public string ReponseAction { get; set; }
    public DateTime DateHeureAction { get; set; }
    
    
    public List<Admin> Admins { get; set; }
    public User User { get; set; }
}