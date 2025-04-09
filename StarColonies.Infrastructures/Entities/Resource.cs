namespace StarColonies.Infrastructures.Entities;

public class Resource
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public int Quantity { get; set; }
    
    // clés étrangères
    public int IdTypeResource { get; set; }
    public TypeResource TypeResource { get; set; } = null!;
}