namespace StarColonies.Infrastructures.Entities;

public class Admin
{
    public int IdAdmin { get; set; }
    public string Nom {get; set;}
    public string Email {get; set;}
    public string Password {get; set;}
    
    List<JournalActivite> journalActivites {get; set;}
}