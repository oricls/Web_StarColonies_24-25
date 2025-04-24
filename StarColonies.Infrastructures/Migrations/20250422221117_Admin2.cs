using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarColonies.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class Admin2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Admin");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Admin");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Admin");

            migrationBuilder.AddColumn<string>(
                name: "ColonId",
                table: "Admin",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RoleId",
                table: "Admin",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Admin_ColonId",
                table: "Admin",
                column: "ColonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Admin_RoleId",
                table: "Admin",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Admin_AspNetRoles_RoleId",
                table: "Admin",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Admin_AspNetUsers_ColonId",
                table: "Admin",
                column: "ColonId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admin_AspNetRoles_RoleId",
                table: "Admin");

            migrationBuilder.DropForeignKey(
                name: "FK_Admin_AspNetUsers_ColonId",
                table: "Admin");

            migrationBuilder.DropIndex(
                name: "IX_Admin_ColonId",
                table: "Admin");

            migrationBuilder.DropIndex(
                name: "IX_Admin_RoleId",
                table: "Admin");

            migrationBuilder.DropColumn(
                name: "ColonId",
                table: "Admin");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Admin");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Admin",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Admin",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Admin",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
