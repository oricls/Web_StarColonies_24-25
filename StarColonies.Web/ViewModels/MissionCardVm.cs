namespace StarColonies.Web.ViewModels;

public record MissionCardVm(
    string Name,
    string Description,
    string Image,
    int Level,
    int BestiaireCount,
    string Target,
    IEnumerable<BestiaireIconVm> BestiaireIcons);
    
    