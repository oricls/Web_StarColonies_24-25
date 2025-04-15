using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace StarColonies.Infrastructures.Entities;

public class Colon : IdentityUser
{
    //public int Id { get; set; } -> géré par indentity
    public string DateBirth { get; set; } = String.Empty;
    public int Endurance { get; set; }
    public int Strength { get; set; }
    public int Level { get; set; }
    public string Avatar { get; set; } = String.Empty;
    
    // clés étrangères
    public int IdProfession { get; set; }
    public Profession Profession { get; set; } = null!;
    
    public IList<Team> Teams { get; set; } = new List<Team>();
    public IList<ColonBonus> ColonBonuses { get; set; } = new List<ColonBonus>();
    public IList<ColonResource> ColonResources { get; set; } = new List<ColonResource>();
}