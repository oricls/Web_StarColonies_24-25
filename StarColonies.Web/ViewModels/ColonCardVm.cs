using StarColonies.Domains;

namespace StarColonies.Web.ViewModels;

public class ColonCardVm
{
    // Données du colon
    public Colon? Colon { get; set; }
        
    // Propriétés de la carte
    public bool IsCurrentUser { get; set; }
    public bool IsSelectable { get; set; }
    public bool IsSelected { get; set; }
    public string UserLabel { get; set; } = "Vous";
        
    // Constructeur pratique pour créer la carte à partir d'un colon
    public static ColonCardVm FromColon(Colon colon, bool isCurrentUser, bool isSelected = false)
    {
        return new ColonCardVm
        {
            Colon = colon,
            IsCurrentUser = isCurrentUser,
            IsSelectable = !isCurrentUser,
            IsSelected = isSelected
        };
    }
}