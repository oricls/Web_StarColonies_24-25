using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarColonies.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class AddGainedResourceMision7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MissionResources_Resource_IdRessource",
                table: "MissionResources");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_MissionResources_IdMission_IdRessource",
                table: "MissionResources");

            migrationBuilder.RenameColumn(
                name: "IdRessource",
                table: "MissionResources",
                newName: "IdResource");

            migrationBuilder.RenameIndex(
                name: "IX_MissionResources_IdRessource",
                table: "MissionResources",
                newName: "IX_MissionResources_IdResource");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_MissionResources_IdMission_IdResource",
                table: "MissionResources",
                columns: new[] { "IdMission", "IdResource" });

            migrationBuilder.AddForeignKey(
                name: "FK_MissionResources_Resource_IdResource",
                table: "MissionResources",
                column: "IdResource",
                principalTable: "Resource",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MissionResources_Resource_IdResource",
                table: "MissionResources");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_MissionResources_IdMission_IdResource",
                table: "MissionResources");

            migrationBuilder.RenameColumn(
                name: "IdResource",
                table: "MissionResources",
                newName: "IdRessource");

            migrationBuilder.RenameIndex(
                name: "IX_MissionResources_IdResource",
                table: "MissionResources",
                newName: "IX_MissionResources_IdRessource");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_MissionResources_IdMission_IdRessource",
                table: "MissionResources",
                columns: new[] { "IdMission", "IdRessource" });

            migrationBuilder.AddForeignKey(
                name: "FK_MissionResources_Resource_IdRessource",
                table: "MissionResources",
                column: "IdRessource",
                principalTable: "Resource",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
