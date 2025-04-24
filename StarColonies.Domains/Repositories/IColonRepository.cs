namespace StarColonies.Domains.Repositories;

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
    Task<IReadOnlyList<Profession>> GetAllProfessionsAsync();
        Task UpdateResourceQuantityAsync(string colonId, int resourceId, int newQuantity);
    Task ExpireBonusAsync(string colonId, int bonusId);
    Task ChangePassword(string colonId, string newPswd);
}