using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarColonies.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class AddGainedResourceMision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MissionResource",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdMission = table.Column<int>(type: "int", nullable: false),
                    IdRessource = table.Column<int>(type: "int", nullable: false),
                    MissionId = table.Column<int>(type: "int", nullable: true),
                    RessourceId = table.Column<int>(type: "int", nullable: true)
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
                        name: "FK_MissionResource_Mission_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Mission",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MissionResource_Resource_RessourceId",
                        column: x => x.RessourceId,
                        principalTable: "Resource",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MissionResource_IdMission",
                table: "MissionResource",
                column: "IdMission");

            migrationBuilder.CreateIndex(
                name: "IX_MissionResource_MissionId",
                table: "MissionResource",
                column: "MissionId");

            migrationBuilder.CreateIndex(
                name: "IX_MissionResource_RessourceId",
                table: "MissionResource",
                column: "RessourceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MissionResource");
        }
    }
}
