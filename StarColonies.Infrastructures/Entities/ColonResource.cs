using System.ComponentModel.DataAnnotations;

namespace StarColonies.Infrastructures.Entities;

public class ColonResource
{
    [Key] // Pour éviter de faire une idiote ColonResourceId
    public string ColonId { get; set; } = string.Empty; // Clé étrangère vers Colon (IdentityUser a une clé de type string)
    public Colon Colon { get; set; } = null!;
    
    [Key] // Idem
    public int ResourceId { get; set; } // Clé étrangère vers Resource
    public Resource Resource { get; set; } = null!;
    
    public int Quantity { get; set; } // La quantité possédée
}