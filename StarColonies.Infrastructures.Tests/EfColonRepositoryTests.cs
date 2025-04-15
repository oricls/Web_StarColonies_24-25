using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using StarColonies.Domains;

namespace StarColonies.Infrastructures.Tests;

[TestClass]
public class EfColonRepositoryTests
{
    private SqliteConnection _connection;
    private StarColoniesContext _context;
    private EfColonRepository _repository;
    
    // ID d'un colon de test créé
    private string _testColonId = "testColonId";
    private int _testResourceId;
    private int _testBonusId;

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
        
        // Tests d'infra donc pas de mock d'Identity...
        _repository = new EfColonRepository(_context, null); // UserManager est remplacé par null 
        
        // Créer les données de test
        CreateTestData();
    }

    [TestCleanup]
    public void Cleanup()
    {
        _context.Dispose();
        _connection.Dispose();
    }
    
    private void CreateTestData()
    {
        // Créer une profession de test
        var profession = new Entities.Profession
        {
            Name = "Profession de test",
            Description = "Description de la profession de test"
        };
        _context.Profession.Add(profession);
        _context.SaveChanges();
        
        // Créer un colon de test directement dans la base de données
        var colon = new Entities.Colon
        {
            Id = _testColonId,
            UserName = "Test Colon",
            Email = "test@colon.com",
            PasswordHash = "HashedPassword",
            DateBirth = "2000-01-01",
            Endurance = 15,
            Strength = 20,
            Level = 10,
            Avatar = "test-avatar.png",
            IdProfession = profession.Id,
            Profession = profession
        };
        
        _context.Users.Add(colon);
        _context.SaveChanges();
        
        // Créer un type de ressource
        var typeResource = new Entities.TypeResource
        {
            Name = "Type de ressource test",
            Description = "Description du type de ressource"
        };
        _context.TypeResource.Add(typeResource);
        _context.SaveChanges();
        
        // Créer une ressource de test
        var resourceEntity = new Entities.Resource
        {
            Name = "Ressource de test",
            Description = "Description de la ressource de test",
            IdTypeResource = typeResource.Id,
            TypeResource = typeResource
        };
        _context.Resource.Add(resourceEntity);
        _context.SaveChanges();
        _testResourceId = resourceEntity.Id;
        
        // Créer un bonus de test
        var bonusEntity = new Entities.Bonus
        {
            Name = "Bonus de test",
            Description = "Description du bonus de test",
            DureeParDefaut = TimeSpan.FromHours(24)
        };
        _context.Bonus.Add(bonusEntity);
        _context.SaveChanges();
        _testBonusId = bonusEntity.Id;
        
        // Créer une relation bonus-ressource
        var bonusResource = new Entities.BonusResource
        {
            ResourceId = resourceEntity.Id,
            Resource = resourceEntity,
            BonusId = bonusEntity.Id,
            Bonus = bonusEntity,
            Quantite = 1
        };
        _context.BonusResource.Add(bonusResource);
        
        // Associer une ressource au colon
        var colonResource = new Entities.ColonResource
        {
            ColonId = _testColonId,
            Colon = colon,
            ResourceId = resourceEntity.Id,
            Resource = resourceEntity,
            Quantity = 100
        };
        _context.ColonResource.Add(colonResource);
        
        // Associer un bonus au colon
        var colonBonus = new Entities.ColonBonus
        {
            ColonId = _testColonId,
            Colon = colon,
            BonusId = bonusEntity.Id,
            Bonus = bonusEntity,
            DateAchat = DateTime.UtcNow.AddHours(-1),
            DateExpiration = DateTime.UtcNow.AddHours(23)
        };
        _context.ColonBonus.Add(colonBonus);
        
        _context.SaveChanges();
    }

    #region Tests des méthodes Get

    [TestMethod]
    public async Task GetAllColonsAsync_ShouldReturnColons()
    {
        // Act
        var colons = await _repository.GetAllColonsAsync();
        
        // Assert
        Assert.IsNotNull(colons);
        Assert.IsTrue(colons.Count > 0);
        Assert.IsTrue(colons.Any(c => c.Id == _testColonId));
        Assert.IsTrue(colons.Any(c => c.Name == "Test Colon"));
    }
    
    [TestMethod]
    public async Task GetColonByIdAsync_ShouldReturnColon()
    {
        // Act
        var colon = await _repository.GetColonByIdAsync(_testColonId);
        
        // Assert
        Assert.IsNotNull(colon);
        Assert.AreEqual(_testColonId, colon.Id);
        Assert.AreEqual("Test Colon", colon.Name);
        Assert.AreEqual("test@colon.com", colon.Email);
        Assert.AreEqual(string.Empty, colon.Password); 
    }
    
    [TestMethod]
    public async Task GetColonByIdAsync_WithInvalidId_ShouldReturnNull()
    {
        // Act
        var colon = await _repository.GetColonByIdAsync("invalidId");
        
        // Assert
        Assert.IsNull(colon);
    }
    
    [TestMethod]
    public async Task GetColonByEmailAsync_ShouldReturnColon()
    {
        // Act
        var colon = await _repository.GetColonByEmailAsync("test@colon.com");
        
        // Assert
        Assert.IsNotNull(colon);
        Assert.AreEqual(_testColonId, colon.Id);
        Assert.AreEqual("Test Colon", colon.Name);
    }
    
    [TestMethod]
    public async Task GetColonByEmailAsync_WithInvalidEmail_ShouldReturnNull()
    {
        // Act
        var colon = await _repository.GetColonByEmailAsync("invalid@email.com");
        
        // Assert
        Assert.IsNull(colon);
    }
    
    [TestMethod]
    public async Task GetColonResourcesAsync_ShouldReturnResources()
    {
        // Act
        var resources = await _repository.GetColonResourcesAsync(_testColonId);
        
        // Assert
        Assert.IsNotNull(resources);
        Assert.AreEqual(1, resources.Count);
        Assert.AreEqual("Ressource de test", resources[0].Name);
        Assert.AreEqual(100, resources[0].Quantity);
        Assert.AreEqual("Type de ressource test", resources[0].TypeName);
    }
    
    [TestMethod]
    public async Task GetColonActiveBonusesAsync_ShouldReturnActiveBonuses()
    {
        // Act
        var bonuses = await _repository.GetColonActiveBonusesAsync(_testColonId);
        
        // Assert
        Assert.IsNotNull(bonuses);
        Assert.AreEqual(1, bonuses.Count);
        Assert.AreEqual("Bonus de test", bonuses[0].Name);
        Assert.IsTrue(bonuses[0].DateExpiration > DateTime.UtcNow);
        
        // Vérifier les ressources associées au bonus
        Assert.IsNotNull(bonuses[0].Resources);
        Assert.AreEqual(1, bonuses[0].Resources.Count);
        Assert.AreEqual("Ressource de test", bonuses[0].Resources[0].ResourceName);
        Assert.AreEqual(1, bonuses[0].Resources[0].Multiplier);
    }

    #endregion

    #region Tests des méthodes Update
    
    [TestMethod]
    public async Task UpdateColonAsync_ShouldUpdateColon()
    {
        // Arrange
        var colonToUpdate = new Colon
        {
            Id = _testColonId,
            Name = "Colon Mis à Jour",
            Email = "updated@colon.com",
            Password = "UpdatedPassword123!"
        };
        
        // Act
        await _repository.UpdateColonAsync(colonToUpdate);
        
        // Assert
        var updatedColon = await _context.Users.FindAsync(_testColonId);
        Assert.IsNotNull(updatedColon);
        Assert.AreEqual("Colon Mis à Jour", updatedColon.UserName);
        Assert.AreEqual("updated@colon.com", updatedColon.Email);
        Assert.AreEqual("HashedPassword", updatedColon.PasswordHash); // Le mot de passe ne doit pas changer
    }
    
    [TestMethod]
    [ExpectedException(typeof(KeyNotFoundException))]
    public async Task UpdateColonAsync_WithInvalidId_ShouldThrowException()
    {
        // Arrange
        var invalidColon = new Colon
        {
            Id = "invalidId",
            Name = "Invalid Colon",
            Email = "invalid@colon.com",
            Password = "InvalidPassword123!"
        };
        
        // Act
        await _repository.UpdateColonAsync(invalidColon);
    }
    
    [TestMethod]
    public async Task DeleteColonAsync_ShouldDeleteColon()
    {
        // Act
        await _repository.DeleteColonAsync(_testColonId);
        
        // Assert
        var deletedColon = await _context.Users.FindAsync(_testColonId);
        Assert.IsNull(deletedColon);
    }
    
    [TestMethod]
    [ExpectedException(typeof(KeyNotFoundException))]
    public async Task DeleteColonAsync_WithInvalidId_ShouldThrowException()
    {
        // Act
        await _repository.DeleteColonAsync("invalidId");
    }

    #endregion

    #region Tests des méthodes de gestion des ressources et bonus

    [TestMethod]
    public async Task AddResourceToColonAsync_NewResource_ShouldAddResource()
    {
        // Arrange
        // Créer une nouvelle ressource
        var typeResource = _context.TypeResource.First();
        var newResourceEntity = new Entities.Resource
        {
            Name = "Nouvelle Ressource",
            Description = "Description de la nouvelle ressource",
            IdTypeResource = typeResource.Id,
            TypeResource = typeResource
        };
        _context.Resource.Add(newResourceEntity);
        await _context.SaveChangesAsync();
        
        // Act
        await _repository.AddResourceToColonAsync(_testColonId, newResourceEntity.Id, 50);
        
        // Assert
        var colonResource = await _context.ColonResource
            .FirstOrDefaultAsync(cr => cr.ColonId == _testColonId && cr.ResourceId == newResourceEntity.Id);
        Assert.IsNotNull(colonResource);
        Assert.AreEqual(50, colonResource.Quantity);
    }
    
    [TestMethod]
    public async Task AddResourceToColonAsync_ExistingResource_ShouldUpdateQuantity()
    {
        // Arrange
        var initialQuantity = _context.ColonResource
            .First(cr => cr.ColonId == _testColonId && cr.ResourceId == _testResourceId).Quantity;
        
        // Act
        await _repository.AddResourceToColonAsync(_testColonId, _testResourceId, 50);
        
        // Assert
        var colonResource = await _context.ColonResource
            .FirstOrDefaultAsync(cr => cr.ColonId == _testColonId && cr.ResourceId == _testResourceId);
        Assert.IsNotNull(colonResource);
        Assert.AreEqual(initialQuantity + 50, colonResource.Quantity);
    }
    
    [TestMethod]
    public async Task AddBonusToColonAsync_ShouldAddBonus()
    {
        // Arrange
        // Créer un nouveau bonus pour éviter le conflit de clé
        var newBonusEntity = new Entities.Bonus
        {
            Name = "Bonus de test 2",
            Description = "Description du second bonus de test",
            DureeParDefaut = TimeSpan.FromHours(48)
        };
        _context.Bonus.Add(newBonusEntity);
        await _context.SaveChangesAsync();
        
        var countBefore = _context.ColonBonus.Count(cb => cb.ColonId == _testColonId);
        
        // Act
        await _repository.AddBonusToColonAsync(_testColonId, newBonusEntity.Id, TimeSpan.FromHours(12));
        
        // Assert
        var countAfter = _context.ColonBonus.Count(cb => cb.ColonId == _testColonId);
        Assert.AreEqual(countBefore + 1, countAfter);
        
        var latestBonus = _context.ColonBonus
            .Where(cb => cb.ColonId == _testColonId && cb.BonusId == newBonusEntity.Id)
            .OrderByDescending(cb => cb.DateAchat)
            .First();
        
        // Vérifier que la durée est bien de 12 heures (approximativement)
        var expectedExpirationHour = latestBonus.DateAchat.AddHours(12).Hour;
        Assert.AreEqual(expectedExpirationHour, latestBonus.DateExpiration.Hour);
    }
    
    [TestMethod]
    public async Task AddBonusToColonAsync_WithZeroDuration_ShouldUseDefaultDuration()
    {
        // Arrange
        // Créer un nouveau bonus pour éviter le conflit de clé
        var newBonusEntity = new Entities.Bonus
        {
            Name = "Bonus de test 3",
            Description = "Description du troisième bonus de test",
            DureeParDefaut = TimeSpan.FromHours(24)
        };
        _context.Bonus.Add(newBonusEntity);
        await _context.SaveChangesAsync();
        
        var countBefore = _context.ColonBonus.Count(cb => cb.ColonId == _testColonId);
        
        // Act
        await _repository.AddBonusToColonAsync(_testColonId, newBonusEntity.Id, TimeSpan.Zero);
        
        // Assert
        var countAfter = _context.ColonBonus.Count(cb => cb.ColonId == _testColonId);
        Assert.AreEqual(countBefore + 1, countAfter);
        
        var latestBonus = _context.ColonBonus
            .Where(cb => cb.ColonId == _testColonId && cb.BonusId == newBonusEntity.Id)
            .OrderByDescending(cb => cb.DateAchat)
            .First();
        
        // Vérifier que la durée par défaut est utilisée (24 heures)
        var expectedExpirationDay = latestBonus.DateAchat.AddHours(24).Day;
        var actualExpirationDay = latestBonus.DateExpiration.Day;
        
        // Vérifier le jour (moins précis mais suffisant pour le test)
        Assert.AreEqual(expectedExpirationDay, actualExpirationDay);
    }
    
    [TestMethod]
    [ExpectedException(typeof(KeyNotFoundException))]
    public async Task AddBonusToColonAsync_WithInvalidBonusId_ShouldThrowException()
    {
        // Act
        await _repository.AddBonusToColonAsync(_testColonId, -1, TimeSpan.FromHours(12));
    }

    #endregion
}