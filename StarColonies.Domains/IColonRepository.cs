namespace StarColonies.Domains;

public interface IColonRepository
{
    Task<IReadOnlyList<Colon>> GetAllColonsAsync();
    Task<Colon> GetColonByIdAsync(string colonId);
    Task<Colon> GetColonByEmailAsync(string email);
    Task CreateColonAsync(Colon colon);
    Task UpdateColonAsync(Colon colon);
    Task DeleteColonAsync(string colonId);
    Task<IReadOnlyList<Resource>> GetColonResourcesAsync(string colonId);
    Task<IReadOnlyList<Bonus>> GetColonActiveBonusesAsync(string colonId);
    Task AddResourceToColonAsync(string colonId, int resourceId, int quantity);
    Task AddBonusToColonAsync(string colonId, int bonusId, TimeSpan duration);
}