using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarColonies.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class AddGainedResourceMision8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MissionResources_Mission_IdMission",
                table: "MissionResources");

            migrationBuilder.DropForeignKey(
                name: "FK_MissionResources_Resource_IdResource",
                table: "MissionResources");

            migrationBuilder.DropForeignKey(
                name: "FK_MissionResources_Resource_ResourceId",
                table: "MissionResources");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_MissionResources_IdMission_IdResource",
                table: "MissionResources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MissionResources",
                table: "MissionResources");

            migrationBuilder.DropIndex(
                name: "IX_MissionResources_ResourceId",
                table: "MissionResources");

            migrationBuilder.DropColumn(
                name: "ResourceId",
                table: "MissionResources");

            migrationBuilder.RenameTable(
                name: "MissionResources",
                newName: "MissionResource");

            migrationBuilder.RenameIndex(
                name: "IX_MissionResources_IdResource",
                table: "MissionResource",
                newName: "IX_MissionResource_IdResource");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MissionResource",
                table: "MissionResource",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MissionResource_IdMission",
                table: "MissionResource",
                column: "IdMission");

            migrationBuilder.AddForeignKey(
                name: "FK_MissionResource_Mission_IdMission",
                table: "MissionResource",
                column: "IdMission",
                principalTable: "Mission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MissionResource_Resource_IdResource",
                table: "MissionResource",
                column: "IdResource",
                principalTable: "Resource",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MissionResource_Mission_IdMission",
                table: "MissionResource");

            migrationBuilder.DropForeignKey(
                name: "FK_MissionResource_Resource_IdResource",
                table: "MissionResource");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MissionResource",
                table: "MissionResource");

            migrationBuilder.DropIndex(
                name: "IX_MissionResource_IdMission",
                table: "MissionResource");

            migrationBuilder.RenameTable(
                name: "MissionResource",
                newName: "MissionResources");

            migrationBuilder.RenameIndex(
                name: "IX_MissionResource_IdResource",
                table: "MissionResources",
                newName: "IX_MissionResources_IdResource");

            migrationBuilder.AddColumn<int>(
                name: "ResourceId",
                table: "MissionResources",
                type: "int",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_MissionResources_IdMission_IdResource",
                table: "MissionResources",
                columns: new[] { "IdMission", "IdResource" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MissionResources",
                table: "MissionResources",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MissionResources_ResourceId",
                table: "MissionResources",
                column: "ResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_MissionResources_Mission_IdMission",
                table: "MissionResources",
                column: "IdMission",
                principalTable: "Mission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MissionResources_Resource_IdResource",
                table: "MissionResources",
                column: "IdResource",
                principalTable: "Resource",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MissionResources_Resource_ResourceId",
                table: "MissionResources",
                column: "ResourceId",
                principalTable: "Resource",
                principalColumn: "Id");
        }
    }
}
