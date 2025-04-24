namespace StarColonies.Infrastructures.Entities;

public class BonusTransaction
{
    public int Id { get; set; }
    public string ColonId { get; set; } = string.Empty;
    public Colon? Colon { get; set; }
    public int BonusId { get; set; }
    public Bonus? Bonus { get; set; }
    public DateTime TransactionDate { get; set; }
        
    // Navigation pour les ressources utilisées dans cette transaction
    public IList<BonusTransactionResource> TransactionResources { get; set; } = new List<BonusTransactionResource>();
}