namespace StarColonies.Domains;

public interface IBonusRepository
{
    Task CreateBonusAsync(Bonus bonus);
    Task DeleteBonusAsync(Bonus bonus);
    Task<IReadOnlyList<Bonus>> GetAllBonusAsync();
    Task<Bonus> GetBonusByName(string name);
    Task<IReadOnlyList<Bonus>> GetActivesBonuses();

    Task<TimeSpan> getDurationOfBonus(Bonus bonus);
    
    Task<IReadOnlyList<BonusResource>> GetBonusResources(Bonus bonus);
    
    Task<IReadOnlyList<TransactionInfo>> GetColonTransactionsAsync(string colonId, int? limit = null);
    Task CreateTransactionAsync(string colonId, int bonusId, List<BonusResource> resources);
}