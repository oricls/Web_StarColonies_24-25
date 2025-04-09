namespace StarColonies.Infrastructures.Entities;

public class TypeResource
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    
    // Clé étrangère
    public IList<Resource> Resources { get; set; } = new List<Resource>();
}