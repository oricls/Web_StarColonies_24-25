namespace StarColonies.Infrastructures.Entities;

public class Resource
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    
    // clés étrangères
    public int IdTypeResource { get; set; }
    public TypeResource TypeResource { get; set; } = null!;
    
    public IList<Colon> Colons { get; set; } = new List<Colon>();
}