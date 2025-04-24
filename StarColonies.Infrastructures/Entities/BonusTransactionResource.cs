namespace StarColonies.Infrastructures.Entities;

public class BonusTransactionResource
{
    public int Id { get; set; }
    public int TransactionId { get; set; }
    public BonusTransaction? Transaction { get; set; }
    public int BonusResourceId { get; set; }
    public BonusResource? BonusResource { get; set; }
    public int Quantite { get; set; }
}