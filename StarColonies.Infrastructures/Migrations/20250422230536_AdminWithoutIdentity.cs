using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarColonies.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class AdminWithoutIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admin_AspNetRoles_RoleId",
                table: "Admin");

            migrationBuilder.DropIndex(
                name: "IX_Admin_RoleId",
                table: "Admin");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f5f1ad4e-cd8b-4bcd-96d1-a2404fc76796");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Admin");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5046d7c1-edb9-4955-8449-1577542166cc", "82806a36-751e-4225-92ca-40d6f38c0ffe", "Admin", "ADMIN" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5046d7c1-edb9-4955-8449-1577542166cc");

            migrationBuilder.AddColumn<string>(
                name: "RoleId",
                table: "Admin",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f5f1ad4e-cd8b-4bcd-96d1-a2404fc76796", "d2ca4bd7-28fc-4b6d-936f-457241694015", "Admin", "ADMIN" });

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
        }
    }
}
