using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarColonies.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class AddGainedResourceMision2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MissionResource_Mission_IdMission",
                table: "MissionResource");

            migrationBuilder.DropForeignKey(
                name: "FK_MissionResource_Mission_MissionId",
                table: "MissionResource");

            migrationBuilder.DropForeignKey(
                name: "FK_MissionResource_Resource_RessourceId",
                table: "MissionResource");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MissionResource",
                table: "MissionResource");

            migrationBuilder.DropIndex(
                name: "IX_MissionResource_IdMission",
                table: "MissionResource");

            migrationBuilder.DropIndex(
                name: "IX_MissionResource_RessourceId",
                table: "MissionResource");

            migrationBuilder.DropColumn(
                name: "RessourceId",
                table: "MissionResource");

            migrationBuilder.RenameTable(
                name: "MissionResource",
                newName: "MissionResources");

            migrationBuilder.RenameIndex(
                name: "IX_MissionResource_MissionId",
                table: "MissionResources",
                newName: "IX_MissionResources_MissionId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_MissionResources_IdMission_IdRessource",
                table: "MissionResources",
                columns: new[] { "IdMission", "IdRessource" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MissionResources",
                table: "MissionResources",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MissionResources_IdRessource",
                table: "MissionResources",
                column: "IdRessource");

            migrationBuilder.AddForeignKey(
                name: "FK_MissionResources_Mission_IdMission",
                table: "MissionResources",
                column: "IdMission",
                principalTable: "Mission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MissionResources_Mission_MissionId",
                table: "MissionResources",
                column: "MissionId",
                principalTable: "Mission",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MissionResources_Resource_IdRessource",
                table: "MissionResources",
                column: "IdRessource",
                principalTable: "Resource",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MissionResources_Mission_IdMission",
                table: "MissionResources");

            migrationBuilder.DropForeignKey(
                name: "FK_MissionResources_Mission_MissionId",
                table: "MissionResources");

            migrationBuilder.DropForeignKey(
                name: "FK_MissionResources_Resource_IdRessource",
                table: "MissionResources");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_MissionResources_IdMission_IdRessource",
                table: "MissionResources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MissionResources",
                table: "MissionResources");

            migrationBuilder.DropIndex(
                name: "IX_MissionResources_IdRessource",
                table: "MissionResources");

            migrationBuilder.RenameTable(
                name: "MissionResources",
                newName: "MissionResource");

            migrationBuilder.RenameIndex(
                name: "IX_MissionResources_MissionId",
                table: "MissionResource",
                newName: "IX_MissionResource_MissionId");

            migrationBuilder.AddColumn<int>(
                name: "RessourceId",
                table: "MissionResource",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MissionResource",
                table: "MissionResource",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MissionResource_IdMission",
                table: "MissionResource",
                column: "IdMission");

            migrationBuilder.CreateIndex(
                name: "IX_MissionResource_RessourceId",
                table: "MissionResource",
                column: "RessourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_MissionResource_Mission_IdMission",
                table: "MissionResource",
                column: "IdMission",
                principalTable: "Mission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MissionResource_Mission_MissionId",
                table: "MissionResource",
                column: "MissionId",
                principalTable: "Mission",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MissionResource_Resource_RessourceId",
                table: "MissionResource",
                column: "RessourceId",
                principalTable: "Resource",
                principalColumn: "Id");
        }
    }
}
