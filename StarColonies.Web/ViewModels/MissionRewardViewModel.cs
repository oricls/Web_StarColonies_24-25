namespace StarColonies.Web.ViewModels;

public class MissionRewardViewModel
{
    public string MissionName { get; set; }
    public List<ColonReward> ColonRewards { get; set; } = new();
}

public class ColonReward
{
    public string ColonName { get; set; }
    public List<ResourceReward> Resources { get; set; } = new();
    public bool LevelGained { get; set; }
}

public class ResourceReward
{
    public string ResourceName { get; set; }
    public string Icon { get; set; } // ex: "fa-gem", "fa-coins"
    public int Quantity { get; set; }
    public string Color { get; set; } = "#ffffff"; // Couleur optionnelle
}