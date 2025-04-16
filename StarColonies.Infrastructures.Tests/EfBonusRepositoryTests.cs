using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using StarColonies.Domains;

namespace StarColonies.Infrastructures.Tests;

[TestClass]
public class EfBonusRepositoryTests
{
    // j'ai fait un copié-collé par flemme
    private SqliteConnection _connection;
    private StarColoniesContext _context;
    private IBonusRepository _repository;

    [TestInitialize]
    public void Initialize()
    {
        // Créer une connexion SQLite en mémoire
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();
        
        // Désactiver les contraintes FK pour les tests
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "PRAGMA foreign_keys = 0;";
            command.ExecuteNonQuery();
        }
        
        // Créer le contexte
        var options = new DbContextOptionsBuilder<StarColoniesContext>()
            .UseSqlite(_connection)
            .Options;
        
        _context = new StarColoniesContext(options);
        _context.Database.EnsureCreated();
        
        // Créer le repository
        _repository = new Infrastructures.EfBonusRepository(_context);
        
    }
    
    [TestCleanup]
    public void Cleanup()
    {
        _context.Dispose();
        _connection.Dispose();
    }
    
    [TestMethod]
    private void CreateTestAddNewBonus()
    {
        var bonusRes = new BonusResource
        {
            ResourceId = 1,
            ResourceName = "bonusResourceTest1",
            Multiplier = 3
        };
        
        var bonus = new Bonus
        {
            Id = 1,
            Name = "bonusTest",
            Description = "description test pour un bonus",
            DateAchat = DateTime.Now,
            DateExpiration = DateTime.Now.AddHours(1),
            Resources = new List<BonusResource>{bonusRes}
        };
        
        _repository.CreateBonusAsync(bonus);
    }
}