namespace StarColonies.Domains.Repositories;

public interface IBonusRepository
{
    Task CreateBonusAsync(Bonus bonus);
    Task DeleteBonusAsync(Bonus bonus);
    Task<IReadOnlyList<Bonus>> GetAllBonusAsync();
    Task<Bonus> GetBonusByName(string name);
    Task<IReadOnlyList<Bonus>> GetActivesBonuses();

    Task<TimeSpan> getDurationOfBonus(Bonus bonus);
    
    Task<IReadOnlyList<BonusResource>> GetBonusResources(Bonus bonus);
}