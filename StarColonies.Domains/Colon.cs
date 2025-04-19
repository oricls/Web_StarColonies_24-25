namespace StarColonies.Domains;

public class Colon
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string  Password { get; set; }
    public string Avatar { get; set; } = string.Empty;
    
    public DateTime DateBirth { get; set; }
    public int Endurance { get; set; }
    public int Strength { get; set; }
    public int Level { get; set; }
    
    public string ProfessionName { get; set; } = string.Empty;

}