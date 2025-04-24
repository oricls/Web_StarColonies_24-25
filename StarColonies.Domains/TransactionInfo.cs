namespace StarColonies.Domains;

public class TransactionInfo
{
    public int Id { get; set; }
    public string ColonId { get; set; } = string.Empty;
    public string ColonName { get; set; } = string.Empty;
    public int BonusId { get; set; }
    public string BonusName { get; set; } = string.Empty;
    public string BonusIconUrl { get; set; } = string.Empty;
    public DateTime TransactionDate { get; set; }
    public IList<TransactionResourceInfo> Resources { get; set; } = new List<TransactionResourceInfo>();
}