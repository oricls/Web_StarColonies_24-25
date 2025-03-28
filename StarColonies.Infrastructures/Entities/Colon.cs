namespace StarColonies.Infrastructures.Entities;

public class Colon
{
    public int IdColon { get; set; }
    
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public int Endurence { get; set; }
    public int Niveau { get; set; }
    public string Avatar { get; set; }
    
    // clés étrangères
    public User IdUser { get; set; } // dans la cas où l'utilisateur serait séparé du colon
    public Profession IdProfession { get; set; }
    public List<Equipe> IdEquipes { get; set; }
    public List<Bonus> IdBonuses { get; set; }
}