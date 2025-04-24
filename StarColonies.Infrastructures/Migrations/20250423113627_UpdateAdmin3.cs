using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarColonies.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAdmin3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Admin_AdminId",
                table: "Logs");

            migrationBuilder.DropIndex(
                name: "IX_Logs_AdminId",
                table: "Logs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d33f5ae-693d-4df3-a09c-dcedc26bcc8e");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Logs");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a8d17110-e8d5-4863-ac6a-5c4eae9c2ac7", "337b9586-39d9-43e9-8bed-327066318218", "Admin", "ADMIN" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a8d17110-e8d5-4863-ac6a-5c4eae9c2ac7");

            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "Logs",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2d33f5ae-693d-4df3-a09c-dcedc26bcc8e", "0a041168-6742-4701-9144-578df0713b91", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_Logs_AdminId",
                table: "Logs",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Admin_AdminId",
                table: "Logs",
                column: "AdminId",
                principalTable: "Admin",
                principalColumn: "Id");
        }
    }
}
