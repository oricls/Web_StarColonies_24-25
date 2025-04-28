using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarColonies.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class AddGainedResourceMision5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResourceId",
                table: "MissionResources",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MissionResources_ResourceId",
                table: "MissionResources",
                column: "ResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_MissionResources_Resource_ResourceId",
                table: "MissionResources",
                column: "ResourceId",
                principalTable: "Resource",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MissionResources_Resource_ResourceId",
                table: "MissionResources");

            migrationBuilder.DropIndex(
                name: "IX_MissionResources_ResourceId",
                table: "MissionResources");

            migrationBuilder.DropColumn(
                name: "ResourceId",
                table: "MissionResources");
        }
    }
}
