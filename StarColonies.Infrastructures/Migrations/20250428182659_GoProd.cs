using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StarColonies.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class GoProd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bonus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DureeParDefaut = table.Column<TimeSpan>(type: "time", nullable: false),
                    IconUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EffectTypeId = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bonus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequeteAction = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ResponseAction = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    DateHeureAction = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profession",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Icone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profession", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeBestiaire",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeBestiaire", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeResource",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeResource", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateBirth = table.Column<DateTime>(type: "datetime2", maxLength: 20, nullable: false),
                    Endurance = table.Column<int>(type: "int", nullable: false),
                    Strength = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IdProfession = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Profession_IdProfession",
                        column: x => x.IdProfession,
                        principalTable: "Profession",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bestiaire",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Strength = table.Column<int>(type: "int", nullable: false),
                    Endurance = table.Column<int>(type: "int", nullable: false),
                    IdTypeBestiaire = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bestiaire", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bestiaire_TypeBestiaire_IdTypeBestiaire",
                        column: x => x.IdTypeBestiaire,
                        principalTable: "TypeBestiaire",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Resource",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IdTypeResource = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resource_TypeResource_IdTypeResource",
                        column: x => x.IdTypeResource,
                        principalTable: "TypeResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BonusTransaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColonId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BonusId = table.Column<int>(type: "int", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonusTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BonusTransaction_AspNetUsers_ColonId",
                        column: x => x.ColonId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BonusTransaction_Bonus_BonusId",
                        column: x => x.BonusId,
                        principalTable: "Bonus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ColonBonus",
                columns: table => new
                {
                    ColonId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BonusId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    DateAchat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateExpiration = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColonBonus", x => new { x.ColonId, x.BonusId });
                    table.ForeignKey(
                        name: "FK_ColonBonus_AspNetUsers_ColonId",
                        column: x => x.ColonId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ColonBonus_Bonus_BonusId",
                        column: x => x.BonusId,
                        principalTable: "Bonus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Baniere = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IdColonCreator = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Team_AspNetUsers_IdColonCreator",
                        column: x => x.IdColonCreator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MissionBestiaire",
                columns: table => new
                {
                    IdMission = table.Column<int>(type: "int", nullable: false),
                    IdBestiaire = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionBestiaire", x => new { x.IdMission, x.IdBestiaire });
                    table.ForeignKey(
                        name: "FK_MissionBestiaire_Bestiaire_IdBestiaire",
                        column: x => x.IdBestiaire,
                        principalTable: "Bestiaire",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MissionBestiaire_Mission_IdMission",
                        column: x => x.IdMission,
                        principalTable: "Mission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BonusResource",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BonusId = table.Column<int>(type: "int", nullable: false),
                    ResourceId = table.Column<int>(type: "int", nullable: false),
                    Quantite = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonusResource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BonusResource_Bonus_BonusId",
                        column: x => x.BonusId,
                        principalTable: "Bonus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BonusResource_Resource_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ColonResource",
                columns: table => new
                {
                    ColonId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ResourceId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColonResource", x => new { x.ColonId, x.ResourceId });
                    table.ForeignKey(
                        name: "FK_ColonResource_AspNetUsers_ColonId",
                        column: x => x.ColonId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ColonResource_Resource_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MissionResource",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdMission = table.Column<int>(type: "int", nullable: false),
                    IdResource = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionResource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MissionResource_Mission_IdMission",
                        column: x => x.IdMission,
                        principalTable: "Mission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MissionResource_Resource_IdResource",
                        column: x => x.IdResource,
                        principalTable: "Resource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResultatMission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IssueEndurance = table.Column<double>(type: "float", nullable: false),
                    IssueStrength = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", maxLength: 100, nullable: false),
                    IdMission = table.Column<int>(type: "int", nullable: false),
                    IdTeam = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultatMission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResultatMission_Mission_IdMission",
                        column: x => x.IdMission,
                        principalTable: "Mission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResultatMission_Team_IdTeam",
                        column: x => x.IdTeam,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamColon",
                columns: table => new
                {
                    MembersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TeamsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamColon", x => new { x.MembersId, x.TeamsId });
                    table.ForeignKey(
                        name: "FK_TeamColon_AspNetUsers_MembersId",
                        column: x => x.MembersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamColon_Team_TeamsId",
                        column: x => x.TeamsId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BonusTransactionResource",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionId = table.Column<int>(type: "int", nullable: false),
                    BonusResourceId = table.Column<int>(type: "int", nullable: false),
                    Quantite = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonusTransactionResource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BonusTransactionResource_BonusResource_BonusResourceId",
                        column: x => x.BonusResourceId,
                        principalTable: "BonusResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BonusTransactionResource_BonusTransaction_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "BonusTransaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "Mac-mini-de-Maximilien1/04/2024 10:30:00Admin", "Mac-mini-de-Maximilien1/04/2024 10:30:00Admin", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Bonus",
                columns: new[] { "Id", "Description", "DureeParDefaut", "EffectTypeId", "IconUrl", "Name" },
                values: new object[,]
                {
                    { 1, "Augmente temporairement la force de tous les membres d'une équipe", new TimeSpan(0, 0, 5, 0, 0), 1, "assets/icons/potion.png", "Potion de force" },
                    { 2, "Investit un soldat supplémentaire pour les prochaines missions", new TimeSpan(0, 0, 3, 0, 0), 3, "assets/icons/potion.png", "Coup de pouce" },
                    { 3, "Augmente temporairement l'endurance de tous les membres d'une équipe", new TimeSpan(0, 0, 5, 0, 0), 2, "assets/icons/potion.png", "Potion d'endurance" },
                    { 4, "Double le nombre de ressources obtenues", new TimeSpan(0, 0, 1, 0, 0), 4, "assets/icons/potion.png", "Grâce de Midas" },
                    { 5, "Augmente l'expérience gagnée et permet de gagner un niveau supplémentaire à chaque mission", new TimeSpan(0, 0, 20, 0, 0), 5, "assets/icons/potion.png", "Parchemin de monsieur Swinnen" }
                });

            migrationBuilder.InsertData(
                table: "Bonus",
                columns: new[] { "Id", "Description", "DureeParDefaut", "IconUrl", "Name" },
                values: new object[] { 6, "Elimine instantanément tous les ministres", new TimeSpan(0, 1, 40, 0, 0), "assets/icons/potion.png", "L'item trop cheatés" });

            migrationBuilder.InsertData(
                table: "Mission",
                columns: new[] { "Id", "Description", "Image", "Name" },
                values: new object[,]
                {
                    { 1, "Une ancienne base de recherche abandonnée recèle des créatures étranges et des technologies perdues.", "base_abandonnee.jpg", "Exploration de la base abandonnée" },
                    { 2, "Un monstre marin légendaire menace les colonies côtières. Son élimination est primordiale.", "leviathan_abysses.jpg", "Chasse au Léviathan des abysses" },
                    { 3, "Des scientifiques sont piégés dans une zone contaminée par des créatures expérimentales.", "quarantaine_zone.jpg", "Sauvetage dans la zone de quarantaine" },
                    { 4, "Une entité extraterrestre intelligente a pris le contrôle d'un réacteur nucléaire.", "hegemon_reactor.jpg", "Désactivation du Hégémon" },
                    { 5, "Des créatures extraterrestres ont infesté d'anciennes ruines découvertes sur une lune lointaine.", "ruines_aliennes.jpg", "Nettoyage des ruines aliennes" }
                });

            migrationBuilder.InsertData(
                table: "Profession",
                columns: new[] { "Id", "Description", "Icone", "Name" },
                values: new object[,]
                {
                    { 1, "Un ingénieur est une personne qui conçoit et construit des machines.", "avatars/engineer.png", "Ingénieur" },
                    { 2, "Un médecin est une personne qui soigne les maladies.", "avatars/doctor.png", "Médecin" },
                    { 3, "Un scientifique est une personne qui étudie la science.", "avatars/scientist.png", "Scientifique" },
                    { 4, "Un soldat est une personne qui combat pour son pays.", "avatars/soldier.png", "Soldat" }
                });

            migrationBuilder.InsertData(
                table: "TypeBestiaire",
                columns: new[] { "Id", "Avatar", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Avatar1.png", "Créations mécaniques ou cybernétiques, souvent conçues pour des tâches spécifiques. Peuvent aller des assistants domestiques aux machines de guerre autonomes.", "Robot" },
                    { 2, "icons/naturelle.png", "Êtres organiques issus de l'évolution naturelle, parfaitement adaptés à leur écosystème. Inclut les créatures des forêts, des océans et autres habitats terrestres.", "Naturelle" },
                    { 3, "icons/extraterrestre.png", "Formes de vie originaires d'autres planètes ou dimensions, possédant souvent des caractéristiques biologiques exotiques et des capacités inexplicables.", "Extraterrestre" },
                    { 4, "icons/paranormal.png", "Entités défiant les lois de la physique, souvent liées à des phénomènes spirituels ou énigmatiques. Inclut fantômes, esprits et créatures dimensionnelles.", "Paranormal" },
                    { 5, "icons/animal.png", "Espèces animales terrestres, qu'elles soient communes ou rares. Peuvent inclure des variants évolués ou génétiquement modifiés.", "Animal" },
                    { 6, "icons/experience.png", "Résultats d'expérimentations scientifiques ou magiques, combinant souvent des traits de multiples espèces. Créatures instables aux capacités imprévisibles.", "Expérience" },
                    { 7, "icons/humanoide.png", "Êtres bipèdes à morphologie semblable aux humains, qu'ils soient d'origine naturelle ou artificielle. Peuvent posséder une intelligence avancée et une société structurée.", "Humanoïde" }
                });

            migrationBuilder.InsertData(
                table: "TypeResource",
                columns: new[] { "Id", "Description", "Icon", "Name" },
                values: new object[,]
                {
                    { 1, "Matériau de construction", "icons/materiau.png", "Matériau" },
                    { 2, "Technologie avancée", "icons/technologie.png", "Technologie" },
                    { 3, "Artefact ancien", "icons/artefact.png", "Artefact" },
                    { 4, "Connaissance avancée", "icons/connaissance.png", "Connaissance" },
                    { 5, "Consomable de base", "icons/consomable.png", "Consomable" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Avatar", "ConcurrencyStamp", "DateBirth", "Email", "EmailConfirmed", "Endurance", "IdProfession", "Level", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Strength", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "ID_COLON_1", 0, "avatar_1.png", "03ca1f88-b451-4304-a0b6-4cbac1f31a68", new DateTime(2005, 4, 28, 20, 26, 59, 177, DateTimeKind.Local).AddTicks(7120), "alex.striker@example.com", true, 2, 1, 0, false, null, "ALEX.STRIKER@EXAMPLE.COM", "ALEX STRIKER", "AQAAAAIAAYagAAAAEEVUetnBp7FeFM6yy0sT4Uq2ss81sXrB30s0MzgXzwAar5RP4+/aWYQ54EVMa8ifkQ==", null, false, "faa7d029-d798-4ad7-8b15-08dff23ce8ea", 5, false, "Alex Striker" },
                    { "ID_COLON_10", 0, "avatar_10.png", "447a4065-f665-49f3-a235-97197257fca9", new DateTime(1998, 4, 28, 20, 26, 59, 463, DateTimeKind.Local).AddTicks(9370), "ryder.nightfall@example.com", true, 5, 1, 0, false, null, "RYDER.NIGHTFALL@EXAMPLE.COM", "RYDER NIGHTFALL", "AQAAAAIAAYagAAAAEHjy+ycaLif5S9WbZ/OMU42k2T3ljsP7aYUmvLhK+98xAwWQhpFrwOcjMUyo8qdp+g==", null, false, "98a9f97a-6976-48ae-b01d-fd318e454d59", 5, false, "Ryder Nightfall" },
                    { "ID_COLON_2", 0, "avatar_2.png", "b14c4d7c-cd5b-4ace-b409-280d39c9b4fb", new DateTime(2000, 4, 28, 20, 26, 59, 210, DateTimeKind.Local).AddTicks(950), "mira.nova@example.com", true, 2, 2, 0, false, null, "MIRA.NOVA@EXAMPLE.COM", "MIRA NOVA", "AQAAAAIAAYagAAAAEJ8mC0iBVruo5Atlav7cE4SjNsLW4QERZVo2YdRwJsjq2NOhadx2Mht1X4zB/zyS/g==", null, false, "d68c94e1-a614-4bce-8872-86e7480719e6", 5, false, "Mira Nova" },
                    { "ID_COLON_3", 0, "avatar_3.png", "489c3fa9-a579-4809-a668-bac2cc7a9dd1", new DateTime(1995, 4, 28, 20, 26, 59, 241, DateTimeKind.Local).AddTicks(9230), "elara.starfinder@example.com", true, 2, 3, 0, false, null, "ELARA.STARFINDER@EXAMPLE.COM", "ELARA STARFINDER", "AQAAAAIAAYagAAAAEJndSlzy0pet9dhD2xPcnPm35q4AuuRIvIQIpY9G5sTqEsu0zrW+Bs5OEm4Biw/RNQ==", null, false, "1e4dbb19-e83b-4d38-8197-d1eef281140f", 5, false, "Elara Starfinder" },
                    { "ID_COLON_4", 0, "avatar_4.png", "a99828c7-45b0-4af6-bdee-c269bc5343f8", new DateTime(1990, 4, 28, 20, 26, 59, 274, DateTimeKind.Local).AddTicks(2490), "jason.blades@example.com", true, 2, 4, 0, false, null, "JASON.BLADES@EXAMPLE.COM", "JASON BLADES", "AQAAAAIAAYagAAAAECE3ba2YtIk46OpipBswVr21fWlN2wXvP2T4s5cfoMnmSsxsG8a7HbQ+3NS6A3gioQ==", null, false, "6a966e87-e0b9-44fd-931e-59b9a0916e39", 5, false, "Jason Blades" },
                    { "ID_COLON_5", 0, "avatar_5.png", "23a6b1d6-473e-416b-90f0-4f485a604cea", new DateTime(1997, 4, 28, 20, 26, 59, 306, DateTimeKind.Local).AddTicks(5210), "lyra.moonshadow@example.com", true, 3, 4, 0, false, null, "LYRA.MOONSHADOW@EXAMPLE.COM", "LYRA MOONSHADOW", "AQAAAAIAAYagAAAAEOynJyOjXGYV0i+/UIeGc09roK//l+f8zyh/keFViDh3qP8ww/vwuyDopx5YwBN5KA==", null, false, "e95bd084-d6d5-4f97-addc-bf0f5d951a97", 4, false, "Lyra Moonshadow" },
                    { "ID_COLON_6", 0, "avatar_6.png", "185fc2bb-4b9a-438b-aa0b-4cc237669027", new DateTime(1993, 4, 28, 20, 26, 59, 337, DateTimeKind.Local).AddTicks(3110), "dax.ironfist@example.com", true, 5, 2, 0, false, null, "DAX.IRONFIST@EXAMPLE.COM", "DAX IRONFIST", "AQAAAAIAAYagAAAAEGcDpKQFA9C8c+Or9q2OylSFhUU8skRPTaGxpq2BExtHxjaBApRRVULNbsneXhaqjA==", null, false, "96596525-997e-4d7b-8340-dc095755e7c5", 6, false, "Dax Ironfist" },
                    { "ID_COLON_7", 0, "avatar_7.png", "ac81e0a4-1e87-4344-a76a-9fa6ee39ce15", new DateTime(1999, 4, 28, 20, 26, 59, 368, DateTimeKind.Local).AddTicks(2120), "zara.flamewalker@example.com", true, 4, 1, 0, false, null, "ZARA.FLAMEWALKER@EXAMPLE.COM", "ZARA FLAMEWALKER", "AQAAAAIAAYagAAAAEGmYHTt55WXfb6Xstvg2hY0MQZtLYe8JYIJJO53oVpmsWolHnG/WH+BRr/Z7vmIZpQ==", null, false, "e6586aaf-3cb6-4739-b321-11c7075c3294", 5, false, "Zara Flamewalker" },
                    { "ID_COLON_8", 0, "avatar_8.png", "c4a1adde-fe2d-40b7-b88b-e68082620eda", new DateTime(1996, 4, 28, 20, 26, 59, 399, DateTimeKind.Local).AddTicks(5170), "orion.starborn@example.com", true, 3, 3, 0, false, null, "ORION.STARBORN@EXAMPLE.COM", "ORION STARBORN", "AQAAAAIAAYagAAAAEC9DIZz5bqsI5jE4d7k8p5K/6+JXpb9hztJgDL/krYznGD6wmNDzB++RXM1hr4xD6Q==", null, false, "1bf6bd08-0ab5-4e2b-bdae-5fce73c092ff", 5, false, "Orion Starborn" },
                    { "ID_COLON_9", 0, "avatar_9.png", "7ae78c90-2a94-4b6b-8e48-d0b578d30f9f", new DateTime(2001, 4, 28, 20, 26, 59, 431, DateTimeKind.Local).AddTicks(5320), "selene.frost@example.com", true, 4, 2, 0, false, null, "SELENE.FROST@EXAMPLE.COM", "SELENE FROST", "AQAAAAIAAYagAAAAEMNnUHeSdct8RlcS1ncenrbTy8/WSSyeLxJDe8ogOYMqThPfF/kA1l1GG1YzMh3T2A==", null, false, "f0df683f-7295-410c-908a-95ec7b13d2fd", 4, false, "Selene Frost" }
                });

            migrationBuilder.InsertData(
                table: "Bestiaire",
                columns: new[] { "Id", "Endurance", "IdTypeBestiaire", "Name", "Strength" },
                values: new object[,]
                {
                    { 1, 2, 1, "Drone", 2 },
                    { 2, 3, 2, "Anomalie", 1 },
                    { 3, 12, 3, "Cryptoïde", 4 },
                    { 4, 1, 4, "Spectre", 7 },
                    { 5, 2, 1, "Nanobot", 1 },
                    { 6, 3, 5, "Prédateur", 4 },
                    { 7, 4, 6, "Chimère", 4 },
                    { 8, 3, 3, "Titan", 5 },
                    { 9, 11, 4, "Entité", 5 },
                    { 10, 4, 7, "Mutant", 2 },
                    { 11, 7, 3, "Leviathan", 9 },
                    { 12, 20, 3, "Hégémon", 20 },
                    { 13, 15, 1, "Drone de combat", 10 }
                });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "Description", "IdTypeResource", "Name" },
                values: new object[,]
                {
                    { 1, "Matériau de protection léger", 1, "Elindage léger" },
                    { 2, "Technologie de stockage d'énergie", 2, "Batterie compacte" },
                    { 3, "Composant technologique avancé", 2, "Module quantique" },
                    { 4, "Matériau haute résistance", 1, "Nanofibres" },
                    { 5, "Matériau de protection renforcé", 1, "Elindage renforcé" },
                    { 6, "Arme technologique", 2, "Mitraillette" },
                    { 7, "Artefact mystérieux", 3, "Gros crâne d'Alex" },
                    { 8, "Matériau suspect", 1, "Residus louches" },
                    { 9, "Substance consommable dégradée", 5, "Vinade avariée" },
                    { 10, "Matériau provenant de dents animales", 1, "Crocs tranchants" },
                    { 11, "Substance consommable bioluminescente", 5, "Sang fluorescent" },
                    { 12, "Connaissance génétique", 4, "ADN cryptoïen" },
                    { 13, "Artefact de protection massive", 3, "Enorme casque" },
                    { 14, "Boisson consommable", 5, "Petite bière" },
                    { 15, "Matériau de protection naturelle", 1, "Ecaille solide" },
                    { 16, "Substance consommable dangereuse", 5, "Venin mortel" },
                    { 17, "Artefact énigmatique", 3, "Cœur de l'Hégémon" },
                    { 18, "Connaissance stratégique", 4, "Plan d'attaque" },
                    { 19, "Matériau spectral", 1, "Membrane fantomique" },
                    { 20, "Connaissance extrasensorielle", 4, "Clairevoyance" }
                });

            migrationBuilder.InsertData(
                table: "MissionBestiaire",
                columns: new[] { "IdBestiaire", "IdMission", "Id" },
                values: new object[,]
                {
                    { 1, 1, 0 },
                    { 2, 1, 0 },
                    { 3, 1, 0 },
                    { 4, 1, 0 },
                    { 5, 2, 0 },
                    { 6, 2, 0 },
                    { 7, 2, 0 },
                    { 8, 2, 0 },
                    { 9, 3, 0 },
                    { 10, 3, 0 },
                    { 11, 3, 0 },
                    { 12, 3, 0 },
                    { 1, 4, 0 },
                    { 2, 4, 0 },
                    { 3, 4, 0 },
                    { 4, 4, 0 },
                    { 5, 4, 0 },
                    { 13, 4, 0 }
                });

            migrationBuilder.InsertData(
                table: "MissionResource",
                columns: new[] { "Id", "IdMission", "IdResource" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 2, 3 },
                    { 4, 2, 4 },
                    { 5, 3, 5 },
                    { 6, 3, 6 },
                    { 7, 4, 7 },
                    { 8, 4, 8 },
                    { 9, 5, 9 },
                    { 10, 5, 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IdProfession",
                table: "AspNetUsers",
                column: "IdProfession");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Bestiaire_IdTypeBestiaire",
                table: "Bestiaire",
                column: "IdTypeBestiaire");

            migrationBuilder.CreateIndex(
                name: "IX_BonusResource_BonusId_ResourceId",
                table: "BonusResource",
                columns: new[] { "BonusId", "ResourceId" });

            migrationBuilder.CreateIndex(
                name: "IX_BonusResource_ResourceId",
                table: "BonusResource",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_BonusTransaction_BonusId",
                table: "BonusTransaction",
                column: "BonusId");

            migrationBuilder.CreateIndex(
                name: "IX_BonusTransaction_ColonId",
                table: "BonusTransaction",
                column: "ColonId");

            migrationBuilder.CreateIndex(
                name: "IX_BonusTransactionResource_BonusResourceId",
                table: "BonusTransactionResource",
                column: "BonusResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_BonusTransactionResource_TransactionId",
                table: "BonusTransactionResource",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_ColonBonus_BonusId",
                table: "ColonBonus",
                column: "BonusId");

            migrationBuilder.CreateIndex(
                name: "IX_ColonResource_ResourceId",
                table: "ColonResource",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_MissionBestiaire_IdBestiaire",
                table: "MissionBestiaire",
                column: "IdBestiaire");

            migrationBuilder.CreateIndex(
                name: "IX_MissionResource_IdMission",
                table: "MissionResource",
                column: "IdMission");

            migrationBuilder.CreateIndex(
                name: "IX_MissionResource_IdResource",
                table: "MissionResource",
                column: "IdResource");

            migrationBuilder.CreateIndex(
                name: "IX_Resource_IdTypeResource",
                table: "Resource",
                column: "IdTypeResource");

            migrationBuilder.CreateIndex(
                name: "IX_ResultatMission_IdMission",
                table: "ResultatMission",
                column: "IdMission");

            migrationBuilder.CreateIndex(
                name: "IX_ResultatMission_IdTeam",
                table: "ResultatMission",
                column: "IdTeam");

            migrationBuilder.CreateIndex(
                name: "IX_Team_IdColonCreator",
                table: "Team",
                column: "IdColonCreator");

            migrationBuilder.CreateIndex(
                name: "IX_TeamColon_TeamsId",
                table: "TeamColon",
                column: "TeamsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BonusTransactionResource");

            migrationBuilder.DropTable(
                name: "ColonBonus");

            migrationBuilder.DropTable(
                name: "ColonResource");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "MissionBestiaire");

            migrationBuilder.DropTable(
                name: "MissionResource");

            migrationBuilder.DropTable(
                name: "ResultatMission");

            migrationBuilder.DropTable(
                name: "TeamColon");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "BonusResource");

            migrationBuilder.DropTable(
                name: "BonusTransaction");

            migrationBuilder.DropTable(
                name: "Bestiaire");

            migrationBuilder.DropTable(
                name: "Mission");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "Resource");

            migrationBuilder.DropTable(
                name: "Bonus");

            migrationBuilder.DropTable(
                name: "TypeBestiaire");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "TypeResource");

            migrationBuilder.DropTable(
                name: "Profession");
        }
    }
}
