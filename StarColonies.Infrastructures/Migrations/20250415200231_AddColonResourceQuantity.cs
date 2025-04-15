using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarColonies.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class AddColonResourceQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColonResource_AspNetUsers_ColonsId",
                table: "ColonResource");

            migrationBuilder.DropForeignKey(
                name: "FK_ColonResource_Resource_ResourcesId",
                table: "ColonResource");

            migrationBuilder.DropColumn(
                name: "NameColon",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ResourcesId",
                table: "ColonResource",
                newName: "ResourceId");

            migrationBuilder.RenameColumn(
                name: "ColonsId",
                table: "ColonResource",
                newName: "ColonId");

            migrationBuilder.RenameIndex(
                name: "IX_ColonResource_ResourcesId",
                table: "ColonResource",
                newName: "IX_ColonResource_ResourceId");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ColonResource",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ColonResource_AspNetUsers_ColonId",
                table: "ColonResource",
                column: "ColonId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ColonResource_Resource_ResourceId",
                table: "ColonResource",
                column: "ResourceId",
                principalTable: "Resource",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColonResource_AspNetUsers_ColonId",
                table: "ColonResource");

            migrationBuilder.DropForeignKey(
                name: "FK_ColonResource_Resource_ResourceId",
                table: "ColonResource");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ColonResource");

            migrationBuilder.RenameColumn(
                name: "ResourceId",
                table: "ColonResource",
                newName: "ResourcesId");

            migrationBuilder.RenameColumn(
                name: "ColonId",
                table: "ColonResource",
                newName: "ColonsId");

            migrationBuilder.RenameIndex(
                name: "IX_ColonResource_ResourceId",
                table: "ColonResource",
                newName: "IX_ColonResource_ResourcesId");

            migrationBuilder.AddColumn<string>(
                name: "NameColon",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_ColonResource_AspNetUsers_ColonsId",
                table: "ColonResource",
                column: "ColonsId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ColonResource_Resource_ResourcesId",
                table: "ColonResource",
                column: "ResourcesId",
                principalTable: "Resource",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
