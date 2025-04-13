namespace StarColonies.Domains;

public class Bestiaire
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Strength { get; set; }
    public int Endurance { get; set; }
    public string TypeBestiaireName { get; set; } = string.Empty;
    public string TypeBestiaireAvatar { get; set; } = string.Empty;
    public string TypeBestiaireDescription { get; set; } = string.Empty;
}