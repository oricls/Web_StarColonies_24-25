using System.ComponentModel.DataAnnotations;

namespace StarColonies.Infrastructures.Entities;

public class Colon
{
    public int Id { get; set; }
    public string NameColon { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
    public string DateBirth { get; set; } = String.Empty;
    public int Endurance { get; set; }
    public int Strength { get; set; }
    public int Level { get; set; }
    public string Avatar { get; set; } = String.Empty;
    
    // clés étrangères
    public int IdProfession { get; set; }
    public Profession Profession { get; set; } = null!;
    
    public IList<Team> Teams { get; set; } = new List<Team>();
    public IList<Bonus> Bonuses { get; set; } = new List<Bonus>();
    public IList<Resource> Resources { get; set; } = new List<Resource>();
}