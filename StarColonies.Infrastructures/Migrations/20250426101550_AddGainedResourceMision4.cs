using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarColonies.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class AddGainedResourceMision4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MissionResources_Mission_MissionId",
                table: "MissionResources");

            migrationBuilder.DropForeignKey(
                name: "FK_MissionResources_Resource_IdRessource",
                table: "MissionResources");

            migrationBuilder.DropIndex(
                name: "IX_MissionResources_MissionId",
                table: "MissionResources");

            migrationBuilder.DropColumn(
                name: "MissionId",
                table: "MissionResources");

            migrationBuilder.AddForeignKey(
                name: "FK_MissionResources_Resource_IdRessource",
                table: "MissionResources",
                column: "IdRessource",
                principalTable: "Resource",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MissionResources_Resource_IdRessource",
                table: "MissionResources");

            migrationBuilder.AddColumn<int>(
                name: "MissionId",
                table: "MissionResources",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MissionResources_MissionId",
                table: "MissionResources",
                column: "MissionId");

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
    }
}
