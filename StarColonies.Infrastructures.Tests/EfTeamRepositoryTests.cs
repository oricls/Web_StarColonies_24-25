using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using StarColonies.Domains;

namespace StarColonies.Infrastructures.Tests;

[TestClass]
public class EfTeamRepositoryTests
{
    private SqliteConnection _connection;
    private StarColoniesContext _context;
    private ITeamRepository _repository;
    
    // ID d'une équipe de test créée
    private int _testTeamId;
    private string _testColonId;

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
        _repository = new EfTeamRepository(_context);
        
        // Créer une équipe et un colon de test
        CreateTestTeamAndColon();
    }

    [TestCleanup]
    public void Cleanup()
    {
        _context.Dispose();
        _connection.Dispose();
    }
    
    private void CreateTestTeamAndColon()
    {
        // Créer un colon de test
        var colon = new Entities.Colon
        {
            Id = "testColonId",
            UserName = "testColon",
            Email = "test@colon.com",
            PasswordHash = "TestPassword123!",
            Level = 10,
            Strength = 15,
            Endurance = 20,
            Avatar = "test-avatar.png",
            IdProfession = 1
        };
        
        _context.Users.Add(colon);
        _context.SaveChanges();
        _testColonId = colon.Id;
        
        // Créer une équipe de test
        var team = new Entities.Team
        {
            Name = "Équipe de test",
            Logo = "test-logo.png",
            Baniere = "test-banner.png",
            IdColonCreator = _testColonId
        };
        
        _context.Team.Add(team);
        _context.SaveChanges();
        
        _testTeamId = team.Id;
        
        // Ajouter le colon à l'équipe
        team.Members.Add(colon);
        _context.SaveChanges();
    }

    #region Tests des méthodes Get

    [TestMethod]
    public async Task GetTeamById_ShouldReturnTeam()
    {
        // Act
        var team = await _repository.GetTeamById(_testTeamId);
        
        // Assert
        Assert.IsNotNull(team);
        Assert.AreEqual(_testTeamId, team.Id);
        Assert.AreEqual("Équipe de test", team.Name);
        Assert.AreEqual("test-logo.png", team.Logo);
        Assert.AreEqual(1, team.MemberCount);
        Assert.AreEqual(10, team.AverageLevel); // Un seul membre de niveau 10
    }
    
    [TestMethod]
    [ExpectedException(typeof(KeyNotFoundException))]
    public async Task GetTeamById_WithInvalidId_ShouldThrowException()
    {
        // Act
        await _repository.GetTeamById(-1);
    }
    
    [TestMethod]
    public async Task GetAllTeams_ShouldReturnTeams()
    {
        // Act
        var teams = await _repository.GetAllTeams();
        
        // Assert
        Assert.IsNotNull(teams);
        Assert.IsTrue(teams.Count > 0);
        Assert.IsTrue(teams.Any(t => t.Id == _testTeamId));
    }
    
    [TestMethod]
    public async Task GetMembersOfTeam_ShouldReturnMembers()
    {
        // Arrange
        var team = await _repository.GetTeamById(_testTeamId);
        
        // Act
        var members = await _repository.GetMembersOfTeam(team);
        
        // Assert
        Assert.IsNotNull(members);
        Assert.AreEqual(1, members.Count);
        Assert.AreEqual("testColon", members[0].Name);
    }
    
    [TestMethod]
    public async Task GetTeamByColon_ShouldReturnTeams()
    {
        // Arrange
        var colon = new Colon
        {
            Id = _testColonId, // L'ID est déjà une string, pas besoin de conversion
            Name = "Test Colon",
            Email = "test@colon.com",
            Password = "TestPassword123!"
        };
        
        // Act
        var teams = await _repository.GetTeamByColon(colon);
        
        // Assert
        Assert.IsNotNull(teams);
        Assert.AreEqual(1, teams.Count);
        Assert.AreEqual(_testTeamId, teams[0].Id);
    }

    #endregion

    #region Tests des méthodes Create

    [TestMethod]
    public async Task CreateTeamAsync_ShouldAddNewTeam()
    {
        // Arrange
        var countBefore = _context.Team.Count();
        var newTeam = new Team
        {
            Name = "Nouvelle Équipe",
            Logo = "nouvelle-equipe.png",
            Baniere = "deuxieme-equipe-banner.png",
            CreatorId = _testColonId,
            MemberCount = 0,
            AverageLevel = 0,
            IsSelectedForMissions = false
        };
        
        // Act
        await _repository.CreateTeamAsync(newTeam);
        
        // Assert
        var countAfter = _context.Team.Count();
        Assert.AreEqual(countBefore + 1, countAfter);
        
        var retrievedTeam = _context.Team.FirstOrDefault(t => t.Name == "Nouvelle Équipe");
        Assert.IsNotNull(retrievedTeam);
        Assert.AreEqual("nouvelle-equipe.png", retrievedTeam.Logo);
    }

    #endregion

    #region Tests des méthodes Update

    [TestMethod]
    public void UpdateTeamInfo_ShouldUpdateTeam()
    {
        // Arrange
        var teamToUpdate = new Team
        {
            Id = _testTeamId,
            Name = "Équipe mise à jour",
            Logo = "logo-updated.png",
            Baniere = "baniere-updated.png",
            MemberCount = 1,
            AverageLevel = 10,
            IsSelectedForMissions = true
        };
        
        // Act
        _repository.UpdateTeamInfo(teamToUpdate);
        
        // Assert
        var updatedTeam = _context.Team.Find(_testTeamId);
        Assert.IsNotNull(updatedTeam);
        Assert.AreEqual("Équipe mise à jour", updatedTeam.Name);
        Assert.AreEqual("logo-updated.png", updatedTeam.Logo);
        Assert.AreEqual("baniere-updated.png", updatedTeam.Baniere); // La bannière est mise à jour avec le logo
    }

    #endregion

    #region Tests des méthodes Delete

    [TestMethod]
    public void DeleteTeam_ShouldRemoveTeam()
    {
        // Arrange
        var team = new Team
        {
            Id = _testTeamId,
            Name = "Équipe de test",
            Logo = "test-logo.png",
            MemberCount = 1,
            AverageLevel = 10,
            IsSelectedForMissions = false
        };
        
        // Act
        _repository.DeleteTeamAsync(team);
        
        // Assert
        var deletedTeam = _context.Team.Find(_testTeamId);
        Assert.IsNull(deletedTeam);
    }
    
    [TestMethod]
    public async Task DeleteTeamAsync_ShouldRemoveTeam()
    {
        // Arrange
        var team = await _repository.GetTeamById(_testTeamId);
        
        // Act
        await _repository.DeleteTeamAsync(team);
        
        // Assert
        var deletedTeam = await _context.Team.FindAsync(_testTeamId);
        Assert.IsNull(deletedTeam);
    }
    
    [TestMethod]
    [ExpectedException(typeof(NullReferenceException))]
    public async Task DeleteTeamAsync_WithInvalidId_ShouldThrowException()
    {
        // Arrange
        var invalidTeam = new Team
        {
            Id = -1,
            Name = "Invalid Team",
            Logo = "invalid.png",
            MemberCount = 0,
            AverageLevel = 0,
            IsSelectedForMissions = false
        };
        
        // Act
        await _repository.DeleteTeamAsync(invalidTeam);
    }

    #endregion

    #region Tests des méthodes de gestion des membres

    [TestMethod]
    public void AddMemberToTeam_ShouldAddMember()
    {
        // Arrange
        // Créer un nouveau colon
        var newColon = new Entities.Colon
        {
            Id = "testColonId2",
            UserName = "secondColon",
            Email = "second@colon.com",
            PasswordHash = "SecondPassword123!",
            Level = 20,
            Strength = 25,
            Endurance = 30,
            Avatar = "second-avatar.png",
            IdProfession = 1
        };
        
        _context.Users.Add(newColon);
        _context.SaveChanges();
        
        var team = new Team
        {
            Id = _testTeamId,
            Name = "Équipe de test",
            Logo = "test-logo.png",
            MemberCount = 1,
            AverageLevel = 10,
            IsSelectedForMissions = false
        };
        
        var colonToAdd = new Colon
        {
            Id = newColon.Id, // Pas de conversion nécessaire
            Name = newColon.UserName,
            Email = newColon.Email,
            Password = newColon.PasswordHash
        };
        
        // Act
        _repository.AddMemberToTeam(team, colonToAdd);
        
        // Assert
        var updatedTeam = _context.Team.Include(t => t.Members).FirstOrDefault(t => t.Id == _testTeamId);
        Assert.IsNotNull(updatedTeam);
        Assert.AreEqual(2, updatedTeam.Members.Count);
        Assert.IsTrue(updatedTeam.Members.Any(m => m.Id == "testColonId2"));
    }
    
    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void AddMemberToTeam_WithExistingMember_ShouldThrowException()
    {
        // Arrange
        var team = new Team
        {
            Id = _testTeamId,
            Name = "Équipe de test",
            Logo = "test-logo.png",
            MemberCount = 1,
            AverageLevel = 10,
            IsSelectedForMissions = false
        };
        
        var existingColon = new Colon
        {
            Id = _testColonId, // ID est déjà une string
            Name = "Test Colon",
            Email = "test@colon.com",
            Password = "TestPassword123!"
        };
        
        // Act
        _repository.AddMemberToTeam(team, existingColon);
    }
    
    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void AddMemberToTeam_WithFullTeam_ShouldThrowException()
    {
        // Arrange
        // Ajouter 4 nouveaux colons à l'équipe (pour un total de 5 avec celui déjà présent)
        for (int i = 2; i <= 5; i++)
        {
            var newColon = new Entities.Colon
            {
                Id = $"testColonId{i}",
                UserName = $"colon{i}",
                Email = $"colon{i}@test.com",
                PasswordHash = $"Password{i}123!",
                Level = 10 + i,
                Strength = 15 + i,
                Endurance = 20 + i,
                Avatar = $"avatar-{i}.png",
                IdProfession = 1
            };
            
            _context.Users.Add(newColon);
            _context.SaveChanges();
            
            var teamEntity = _context.Team.Include(t => t.Members).FirstOrDefault(t => t.Id == _testTeamId);
            teamEntity?.Members.Add(newColon);
            _context.SaveChanges();
        }
        
        // Créer un 6e colon
        var sixthColon = new Entities.Colon
        {
            Id = "testColonId6",
            UserName = "sixthColon",
            Email = "sixth@colon.com",
            PasswordHash = "SixthPassword123!",
            Level = 16,
            Strength = 21,
            Endurance = 26,
            Avatar = "sixth-avatar.png",
            IdProfession = 1
        };
        
        _context.Users.Add(sixthColon);
        _context.SaveChanges();
        
        var team = new Team
        {
            Id = _testTeamId,
            Name = "Équipe de test",
            Logo = "test-logo.png",
            MemberCount = 5, // L'équipe est maintenant pleine
            AverageLevel = 12,
            IsSelectedForMissions = false
        };
        
        var colonToAdd = new Colon
        {
            Id = sixthColon.Id, // Pas de conversion nécessaire
            Name = sixthColon.UserName,
            Email = sixthColon.Email,
            Password = sixthColon.PasswordHash
        };
        
        // Act
        _repository.AddMemberToTeam(team, colonToAdd);
    }
    
    [TestMethod]
    public void RemoveMemberToTeam_ShouldRemoveMember()
    {
        // Arrange
        var team = new Team
        {
            Id = _testTeamId,
            Name = "Équipe de test",
            Logo = "test-logo.png",
            MemberCount = 1,
            AverageLevel = 10,
            IsSelectedForMissions = false
        };
        
        var colonToRemove = new Colon
        {
            Id = _testColonId, // Pas de conversion nécessaire
            Name = "Test Colon",
            Email = "test@colon.com",
            Password = "TestPassword123!"
        };
        
        // Act
        _repository.RemoveMemberToTeam(team, colonToRemove);
        
        // Assert
        var updatedTeam = _context.Team.Include(t => t.Members).FirstOrDefault(t => t.Id == _testTeamId);
        Assert.IsNotNull(updatedTeam);
        Assert.AreEqual(0, updatedTeam.Members.Count);
    }
    
    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void RemoveMemberToTeam_WithNonMember_ShouldThrowException()
    {
        // Arrange
        // Créer un colon qui n'est pas membre de l'équipe
        var nonMemberColon = new Entities.Colon
        {
            Id = "nonMemberColonId",
            UserName = "nonMemberColon",
            Email = "nonmember@colon.com",
            PasswordHash = "NonMemberPassword123!",
            Level = 30,
            Strength = 35,
            Endurance = 40,
            Avatar = "nonmember-avatar.png",
            IdProfession = 1
        };
        
        _context.Users.Add(nonMemberColon);
        _context.SaveChanges();
        
        var team = new Team
        {
            Id = _testTeamId,
            Name = "Équipe de test",
            Logo = "test-logo.png",
            MemberCount = 1,
            AverageLevel = 10,
            IsSelectedForMissions = false
        };
        
        var colonToRemove = new Colon
        {
            Id = nonMemberColon.Id, // Pas de conversion nécessaire
            Name = nonMemberColon.UserName,
            Email = nonMemberColon.Email,
            Password = nonMemberColon.PasswordHash
        };
        
        // Act
        _repository.RemoveMemberToTeam(team, colonToRemove);
    }

    #endregion
}