namespace StarColonies.Domains;

public interface IBonusRepository
{
    Task CreateBonusAsync(Bonus bonus);
    Task DeleteBonusAsync(Bonus bonus);
    Task<IReadOnlyList<Bonus>> GetAllBonusAsync();
    Task<Bonus> GetBonusByName(string name);
    Task<IReadOnlyList<Bonus>> GetActivesBonuses();
    
    
}