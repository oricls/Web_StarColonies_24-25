using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StarColonies.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class SeedMissionWithBestiaire : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLog", x => x.Id);
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
                name: "Admin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ActivityLogId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Admin_ActivityLog_ActivityLogId",
                        column: x => x.ActivityLogId,
                        principalTable: "ActivityLog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequeteAction = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ResponseAction = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    DateHeureAction = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActivityLogId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Log_ActivityLog_ActivityLogId",
                        column: x => x.ActivityLogId,
                        principalTable: "ActivityLog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Colon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameColon = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DateBirth = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Endurance = table.Column<int>(type: "int", nullable: false),
                    Strength = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IdProfession = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Colon_Profession_IdProfession",
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
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Baniere = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IdColonCreator = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Team_Colon_IdColonCreator",
                        column: x => x.IdColonCreator,
                        principalTable: "Colon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MissionBestiaire",
                columns: table => new
                {
                    BestiaireId = table.Column<int>(type: "int", nullable: false),
                    MissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionBestiaire", x => new { x.BestiaireId, x.MissionId });
                    table.ForeignKey(
                        name: "FK_MissionBestiaire_Bestiaire_BestiaireId",
                        column: x => x.BestiaireId,
                        principalTable: "Bestiaire",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MissionBestiaire_Mission_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Mission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bonus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DateHeureAchat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateHeureValidite = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdColon = table.Column<int>(type: "int", nullable: false),
                    IdResource = table.Column<int>(type: "int", nullable: false),
                    QuantiteResource = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bonus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bonus_Colon_IdColon",
                        column: x => x.IdColon,
                        principalTable: "Colon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bonus_Resource_IdResource",
                        column: x => x.IdResource,
                        principalTable: "Resource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ColonResource",
                columns: table => new
                {
                    ColonsId = table.Column<int>(type: "int", nullable: false),
                    ResourcesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColonResource", x => new { x.ColonsId, x.ResourcesId });
                    table.ForeignKey(
                        name: "FK_ColonResource_Colon_ColonsId",
                        column: x => x.ColonsId,
                        principalTable: "Colon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ColonResource_Resource_ResourcesId",
                        column: x => x.ResourcesId,
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
                    Issue = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                    MembersId = table.Column<int>(type: "int", nullable: false),
                    TeamsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamColon", x => new { x.MembersId, x.TeamsId });
                    table.ForeignKey(
                        name: "FK_TeamColon_Colon_MembersId",
                        column: x => x.MembersId,
                        principalTable: "Colon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamColon_Team_TeamsId",
                        column: x => x.TeamsId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                table: "TypeBestiaire",
                columns: new[] { "Id", "Avatar", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Avatar1.png", "Créations mécaniques ou cybernétiques, souvent conçues pour des tâches spécifiques. Peuvent aller des assistants domestiques aux machines de guerre autonomes.", "Robot" },
                    { 2, "Avatar2.png", "Êtres organiques issus de l'évolution naturelle, parfaitement adaptés à leur écosystème. Inclut les créatures des forêts, des océans et autres habitats terrestres.", "Naturelle" },
                    { 3, "Avatar3.png", "Formes de vie originaires d'autres planètes ou dimensions, possédant souvent des caractéristiques biologiques exotiques et des capacités inexplicables.", "Extraterrestre" },
                    { 4, "Avatar4.png", "Entités défiant les lois de la physique, souvent liées à des phénomènes spirituels ou énigmatiques. Inclut fantômes, esprits et créatures dimensionnelles.", "Paranormal" },
                    { 5, "Avatar5.png", "Espèces animales terrestres, qu'elles soient communes ou rares. Peuvent inclure des variants évolués ou génétiquement modifiés.", "Animal" },
                    { 6, "Avatar6.png", "Résultats d'expérimentations scientifiques ou magiques, combinant souvent des traits de multiples espèces. Créatures instables aux capacités imprévisibles.", "Expérience" },
                    { 7, "Avatar7.png", "Êtres bipèdes à morphologie semblable aux humains, qu'ils soient d'origine naturelle ou artificielle. Peuvent posséder une intelligence avancée et une société structurée.", "Humanoïde" }
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
                table: "MissionBestiaire",
                columns: new[] { "BestiaireId", "MissionId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 4 },
                    { 2, 1 },
                    { 2, 4 },
                    { 3, 1 },
                    { 3, 4 },
                    { 4, 1 },
                    { 4, 4 },
                    { 5, 2 },
                    { 5, 4 },
                    { 6, 2 },
                    { 7, 2 },
                    { 8, 2 },
                    { 9, 3 },
                    { 10, 3 },
                    { 11, 3 },
                    { 12, 3 },
                    { 13, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admin_ActivityLogId",
                table: "Admin",
                column: "ActivityLogId");

            migrationBuilder.CreateIndex(
                name: "IX_Bestiaire_IdTypeBestiaire",
                table: "Bestiaire",
                column: "IdTypeBestiaire");

            migrationBuilder.CreateIndex(
                name: "IX_Bonus_IdColon",
                table: "Bonus",
                column: "IdColon");

            migrationBuilder.CreateIndex(
                name: "IX_Bonus_IdResource",
                table: "Bonus",
                column: "IdResource");

            migrationBuilder.CreateIndex(
                name: "IX_Colon_IdProfession",
                table: "Colon",
                column: "IdProfession");

            migrationBuilder.CreateIndex(
                name: "IX_ColonResource_ResourcesId",
                table: "ColonResource",
                column: "ResourcesId");

            migrationBuilder.CreateIndex(
                name: "IX_Log_ActivityLogId",
                table: "Log",
                column: "ActivityLogId");

            migrationBuilder.CreateIndex(
                name: "IX_MissionBestiaire_MissionId",
                table: "MissionBestiaire",
                column: "MissionId");

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
                name: "Admin");

            migrationBuilder.DropTable(
                name: "Bonus");

            migrationBuilder.DropTable(
                name: "ColonResource");

            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "MissionBestiaire");

            migrationBuilder.DropTable(
                name: "ResultatMission");

            migrationBuilder.DropTable(
                name: "TeamColon");

            migrationBuilder.DropTable(
                name: "Resource");

            migrationBuilder.DropTable(
                name: "ActivityLog");

            migrationBuilder.DropTable(
                name: "Bestiaire");

            migrationBuilder.DropTable(
                name: "Mission");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "TypeResource");

            migrationBuilder.DropTable(
                name: "TypeBestiaire");

            migrationBuilder.DropTable(
                name: "Colon");

            migrationBuilder.DropTable(
                name: "Profession");
        }
    }
}
