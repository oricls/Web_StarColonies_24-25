namespace StarColonies.Infrastructures.Entities;

public class Mission
{
    public int IdMission { get; set; }
    public string Nom { get; set; }
    public int Niveau { get; set; }
    
    // clés
    public List<Equipe> Equipe { get; set; }
    public ResultatMission Resultat { get; set; }
}