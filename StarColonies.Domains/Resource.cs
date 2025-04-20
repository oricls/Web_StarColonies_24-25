namespace StarColonies.Domains;

public class Resource
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string TypeName { get; set; }
    public int Quantity { get; set; } // Utilisé lors de la récupération des ressources d'un colon
    public string IconUrl { get; set; }
}