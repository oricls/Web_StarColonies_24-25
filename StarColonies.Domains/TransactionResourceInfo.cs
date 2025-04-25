namespace StarColonies.Domains;

public class TransactionResourceInfo
{
    public int ResourceId { get; set; }
    public string ResourceName { get; set; } = string.Empty;
    public string ResourceType { get; set; } = string.Empty;
    public string IconUrl { get; set; } = string.Empty;
    public int Quantite { get; set; }
}
