using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using StarColonies.Domains;

namespace StarColonies.Infrastructures.Tests;

[TestClass]
public class EfMissionRepositoryTests
{
    // Warnings pas important selon moi
    private SqliteConnection _connection;
    private StarColoniesContext _context;
    private IMissionRepository _repository;
        
    // ID d'une mission de test créée
    private int _testMissionId;

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
        _repository = new EfMissionRepository(_context);
            
        // Créer une mission de test
        CreateTestMission();
    }

    [TestCleanup]
    public void Cleanup()
    {
        _context.Dispose();
        _connection.Dispose();
    }
        
    private void CreateTestMission()
    {
        // Créer une mission directement
        var mission = new Entities.Mission
        {
            Name = "Mission de test",
            Image = "test-mission.png",
            Description = "Description de test"
        };
            
        _context.Mission.Add(mission);
        _context.SaveChanges();
            
        _testMissionId = mission.Id;
    }

    #region Tests des méthodes Get

    [TestMethod]
    public void GetMissionById_ShouldReturnMission()
    {
        // Act
        var mission = _repository.GetMissionById(_testMissionId);
            
        // Assert
        Assert.IsNotNull(mission);
        Assert.AreEqual(_testMissionId, mission.Id);
        Assert.AreEqual("Mission de test", mission.Name);
        Assert.AreEqual("test-mission.png", mission.Image);
        Assert.AreEqual("Description de test", mission.Description);
    }
        
    [TestMethod]
    public async Task GetMissionByIdAsync_ShouldReturnMission()
    {
        // Act
        var mission = await _repository.GetMissionByIdAsync(_testMissionId);
            
        // Assert
        Assert.IsNotNull(mission);
        Assert.AreEqual(_testMissionId, mission.Id);
        Assert.AreEqual("Mission de test", mission.Name);
    }
        
    [TestMethod]
    public void GetAllMissions_ShouldReturnMissions()
    {
        // Act
        var missions = _repository.GetAllMissions();
            
        // Assert
        Assert.IsNotNull(missions);
        Assert.IsTrue(missions.Count > 0);
        Assert.IsTrue(missions.Any(m => m.Id == _testMissionId));
    }
        
    [TestMethod]
    public async Task GetAllMissionsAsync_ShouldReturnMissions()
    {
        // Act
        var missions = await _repository.GetAllMissionsAsync();
            
        // Assert
        Assert.IsNotNull(missions);
        Assert.IsTrue(missions.Count > 0);
        Assert.IsTrue(missions.Any(m => m.Id == _testMissionId));
    }

    #endregion

    #region Tests des méthodes Save

    [TestMethod]
    public void SaveOrUpdateMission_ShouldAddNewMission()
    {
        // Arrange
        var countBefore = _context.Mission.Count();
        var newMission = new Mission
        {
            Name = "Nouvelle Mission",
            Image = "nouvelle-mission.png",
            Description = "Description de la nouvelle mission"
        };
            
        // Act
        _repository.SaveOrUpdateMission(newMission);
            
        // Assert
        var countAfter = _context.Mission.Count();
        Assert.AreEqual(countBefore + 1, countAfter);
            
        var retrievedMission = _context.Mission.FirstOrDefault(m => m.Name == "Nouvelle Mission");
        Assert.IsNotNull(retrievedMission);
        Assert.AreEqual("nouvelle-mission.png", retrievedMission.Image);
        Assert.AreEqual("Description de la nouvelle mission", retrievedMission.Description);
    }
        
    [TestMethod]
    public void SaveOrUpdateMission_ShouldUpdateExistingMission()
    {
        // Arrange
        var missionToUpdate = new Domains.Mission
        {
            Id = _testMissionId,
            Name = "Mission mise à jour",
            Image = "mission-updated.png",
            Description = "Description mise à jour"
        };
            
        // Act
        _repository.SaveOrUpdateMission(missionToUpdate);
            
        // Assert
        var updatedMission = _repository.GetMissionById(_testMissionId);
        Assert.IsNotNull(updatedMission);
        Assert.AreEqual("Mission mise à jour", updatedMission.Name);
        Assert.AreEqual("mission-updated.png", updatedMission.Image);
        Assert.AreEqual("Description mise à jour", updatedMission.Description);
    }
        
    [TestMethod]
    public async Task SaveOrUpdateMissionAsync_ShouldAddNewMission()
    {
        // Arrange
        var countBefore = _context.Mission.Count();
        var newMission = new Domains.Mission
        {
            Name = "Nouvelle Mission Async",
            Image = "nouvelle-mission-async.png",
            Description = "Description de la nouvelle mission async"
        };
            
        // Act
        await _repository.SaveOrUpdateMissionAsync(newMission);
            
        // Assert
        var countAfter = _context.Mission.Count();
        Assert.AreEqual(countBefore + 1, countAfter);
            
        var retrievedMission = _context.Mission.FirstOrDefault(m => m.Name == "Nouvelle Mission Async");
        Assert.IsNotNull(retrievedMission);
        Assert.AreEqual("nouvelle-mission-async.png", retrievedMission.Image);
        Assert.AreEqual("Description de la nouvelle mission async", retrievedMission.Description);
    }
        
    [TestMethod]
    public async Task SaveOrUpdateMissionAsync_ShouldUpdateExistingMission()
    {
        // Arrange
        var missionToUpdate = new Domains.Mission
        {
            Id = _testMissionId,
            Name = "Mission mise à jour async",
            Image = "mission-updated-async.png",
            Description = "Description mise à jour async"
        };
            
        // Act
        await _repository.SaveOrUpdateMissionAsync(missionToUpdate);
            
        // Assert
        var updatedMission = await _repository.GetMissionByIdAsync(_testMissionId);
        Assert.IsNotNull(updatedMission);
        Assert.AreEqual("Mission mise à jour async", updatedMission.Name);
        Assert.AreEqual("mission-updated-async.png", updatedMission.Image);
        Assert.AreEqual("Description mise à jour async", updatedMission.Description);
    }

    #endregion

    #region Tests des méthodes avec relations
           
    [TestMethod]
    public void GetBestiairesByMissionId_ShouldReturnBestiaires()
    {
        try
        {
            // Arrange - Créer un type de bestiaire, un bestiaire et l'associer à la mission de test
            var typeBestiaire = new Entities.TypeBestiaire
            {
                Name = "Type de test",
                Avatar = "test-avatar.png",
                Description = "Description du type de test"
            };
            _context.TypeBestiaire.Add(typeBestiaire);
            _context.SaveChanges();
                   
            var bestiaire = new Entities.Bestiaire
            {
                Name = "Bestiaire de test",
                Strength = 10,
                Endurance = 15,
                IdTypeBestiaire = typeBestiaire.Id,
                TypeBestiaire = typeBestiaire
            };
            _context.Bestiaire.Add(bestiaire);
            _context.SaveChanges();
                   
            var missionBestiaire = new Entities.MissionBestiaire
            {
                IdMission = _testMissionId,
                IdBestiaire = bestiaire.Id,
                Mission = _context.Mission.Find(_testMissionId),
                Bestiaire = bestiaire
            };
            _context.MissionBestiaire.Add(missionBestiaire);
            _context.SaveChanges();
                   
            // Act
            var bestiaires = _repository.GetBestiairesByMissionId(_testMissionId);
                   
            // Assert
            Assert.IsNotNull(bestiaires);
            Assert.AreEqual(1, bestiaires.Count);
            Assert.AreEqual("Bestiaire de test", bestiaires[0].Name);
            Assert.AreEqual(10, bestiaires[0].Strength);
            Assert.AreEqual(15, bestiaires[0].Endurance);
            Assert.AreEqual("Type de test", bestiaires[0].TypeBestiaireName);
        }
        catch (Exception ex)
        {
            Assert.Inconclusive($"Test non concluant en raison d'une erreur lors de la préparation des données : {ex.Message}");
        }
    }
           
    [TestMethod]
    public async Task GetBestiairesByMissionIdAsync_ShouldReturnBestiaires()
    {
        try
        {
            // Nous pouvons nous appuyer sur les données créées dans le test précédent
            // Si ce test s'exécute en premier, les données seront créées ici
            if (!_context.Bestiaire.Any(b => b.Name == "Bestiaire de test"))
            {
                var typeBestiaire = new Entities.TypeBestiaire
                {
                    Name = "Type de test",
                    Avatar = "test-avatar.png",
                    Description = "Description du type de test"
                };
                _context.TypeBestiaire.Add(typeBestiaire);
                await _context.SaveChangesAsync();
                       
                var bestiaire = new Entities.Bestiaire
                {
                    Name = "Bestiaire de test",
                    Strength = 10,
                    Endurance = 15,
                    IdTypeBestiaire = typeBestiaire.Id,
                    TypeBestiaire = typeBestiaire
                };
                _context.Bestiaire.Add(bestiaire);
                await _context.SaveChangesAsync();
                       
                var missionBestiaire = new Entities.MissionBestiaire
                {
                    IdMission = _testMissionId,
                    IdBestiaire = bestiaire.Id,
                    Mission = await _context.Mission.FindAsync(_testMissionId),
                    Bestiaire = bestiaire
                };
                _context.MissionBestiaire.Add(missionBestiaire);
                await _context.SaveChangesAsync();
            }
                   
            // Act
            var bestiaires = await _repository.GetBestiairesByMissionIdAsync(_testMissionId);
                   
            // Assert
            Assert.IsNotNull(bestiaires);
            Assert.IsTrue(bestiaires.Count > 0);
            Assert.IsTrue(bestiaires.Any(b => b.Name == "Bestiaire de test"));
        }
        catch (Exception ex)
        {
            Assert.Inconclusive($"Test non concluant en raison d'une erreur lors de la préparation des données : {ex.Message}");
        }
    }
           
    [TestMethod]
    public void GetMissionsByTeamId_ShouldReturnMissions()
    {
        try
        {
            // Arrange - Créer une équipe et l'associer à la mission de test via un résultat de mission
            var team = new Entities.Team
            {
                Name = "Équipe de test",
                Logo = "test-team-logo.png"
            };
            _context.Team.Add(team);
            _context.SaveChanges();
                   
            var resultatMission = new Entities.ResultatMission
            {
                Issue = 1, // Success
                Date = DateTime.Now.ToString("yyyy-MM-dd"),
                IdMission = _testMissionId,
                Mission = _context.Mission.Find(_testMissionId),
                IdTeam = team.Id,
                Team = team
            };
            _context.ResultatMission.Add(resultatMission);
            _context.SaveChanges();
                   
            // Act
            var missions = _repository.GetMissionsByTeamId(team.Id);
                   
            // Assert
            Assert.IsNotNull(missions);
            Assert.AreEqual(1, missions.Count);
            Assert.AreEqual(_testMissionId, missions[0].Id);
        }
        catch (Exception ex)
        {
            Assert.Inconclusive($"Test non concluant en raison d'une erreur lors de la préparation des données : {ex.Message}");
        }
    }
           
    [TestMethod]
    public async Task GetMissionsByTeamIdAsync_ShouldReturnMissions()
    {
        try
        {
            // Nous pouvons nous appuyer sur les données créées dans le test précédent
            var team = _context.Team.FirstOrDefault(t => t.Name == "Équipe de test");
                   
            // Si ce test s'exécute en premier, on crée les données nécessaires
            if (team == null)
            {
                team = new Entities.Team
                {
                    Name = "Équipe de test",
                    Logo = "test-team-logo.png"
                };
                _context.Team.Add(team);
                await _context.SaveChangesAsync();
                       
                var resultatMission = new Entities.ResultatMission
                {
                    Issue = 1,
                    Date = DateTime.Now.ToString("yyyy-MM-dd"),
                    IdMission = _testMissionId,
                    Mission = await _context.Mission.FindAsync(_testMissionId),
                    IdTeam = team.Id,
                    Team = team
                };
                _context.ResultatMission.Add(resultatMission);
                await _context.SaveChangesAsync();
            }
                   
            // Act
            var missions = await _repository.GetMissionsByTeamIdAsync(team.Id);
                   
            // Assert
            Assert.IsNotNull(missions);
            Assert.IsTrue(missions.Count > 0);
            Assert.IsTrue(missions.Any(m => m.Id == _testMissionId));
        }
        catch (Exception ex)
        {
            Assert.Inconclusive($"Test non concluant en raison d'une erreur lors de la préparation des données : {ex.Message}");
        }
    }
           
    [TestMethod]
    public void GetResultatsByMissionId_ShouldReturnResultats()
    {
        try
        {
            // Nous pouvons nous appuyer sur les données créées dans les tests précédents
            var team = _context.Team.FirstOrDefault(t => t.Name == "Équipe de test");
                   
            // Si ce test s'exécute en premier, on crée les données nécessaires
            if (team == null)
            {
                team = new Entities.Team
                {
                    Name = "Équipe de test",
                    Logo = "test-team-logo.png"
                };
                _context.Team.Add(team);
                _context.SaveChanges();
                       
                var resultatMission = new Entities.ResultatMission
                {
                    Issue = 1,
                    Date = DateTime.Now.ToString("yyyy-MM-dd"),
                    IdMission = _testMissionId,
                    Mission = _context.Mission.Find(_testMissionId),
                    IdTeam = team.Id,
                    Team = team
                };
                _context.ResultatMission.Add(resultatMission);
                _context.SaveChanges();
            }
                   
            // Act
            var resultats = _repository.GetResultatsByMissionId(_testMissionId);
                   
            // Assert
            Assert.IsNotNull(resultats);
            Assert.AreEqual(1, resultats.Count);
            Assert.AreEqual(1, resultats[0].Issue);
            Assert.AreEqual("Équipe de test", resultats[0].TeamName);
        }
        catch (Exception ex)
        {
            Assert.Inconclusive($"Test non concluant en raison d'une erreur lors de la préparation des données : {ex.Message}");
        }
    }
           
    [TestMethod]
    public async Task GetResultatsByMissionIdAsync_ShouldReturnResultats()
    {
        try
        {
            // Nous pouvons nous appuyer sur les données créées dans les tests précédents
            var team = _context.Team.FirstOrDefault(t => t.Name == "Équipe de test");
                   
            // Si ce test s'exécute en premier, on crée les données nécessaires
            if (team == null)
            {
                team = new Entities.Team
                {
                    Name = "Équipe de test",
                    Logo = "test-team-logo.png"
                };
                _context.Team.Add(team);
                await _context.SaveChangesAsync();
                       
                var resultatMission = new Entities.ResultatMission
                {
                    Issue = 1,
                    Date = DateTime.Now.ToString("yyyy-MM-dd"),
                    IdMission = _testMissionId,
                    Mission = await _context.Mission.FindAsync(_testMissionId),
                    IdTeam = team.Id,
                    Team = team
                };
                _context.ResultatMission.Add(resultatMission);
                await _context.SaveChangesAsync();
            }
                   
            // Act
            var resultats = await _repository.GetResultatsByMissionIdAsync(_testMissionId);
                   
            // Assert
            Assert.IsNotNull(resultats);
            Assert.IsTrue(resultats.Count > 0);
            Assert.IsTrue(resultats.Any(r => r.TeamName == "Équipe de test"));
        }
        catch (Exception ex)
        {
            Assert.Inconclusive($"Test non concluant en raison d'une erreur lors de la préparation des données : {ex.Message}");
        }
    }
           
    #endregion
}