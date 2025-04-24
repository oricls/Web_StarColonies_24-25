using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarColonies.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admin_ActivityLog_ActivityLogId",
                table: "Admin");

            migrationBuilder.DropForeignKey(
                name: "FK_Log_ActivityLog_ActivityLogId",
                table: "Log");

            migrationBuilder.DropTable(
                name: "ActivityLog");

            migrationBuilder.DropIndex(
                name: "IX_Admin_ActivityLogId",
                table: "Admin");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Log",
                table: "Log");

            migrationBuilder.DropIndex(
                name: "IX_Log_ActivityLogId",
                table: "Log");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5046d7c1-edb9-4955-8449-1577542166cc");

            migrationBuilder.DropColumn(
                name: "ActivityLogId",
                table: "Admin");

            migrationBuilder.DropColumn(
                name: "ActivityLogId",
                table: "Log");

            migrationBuilder.RenameTable(
                name: "Log",
                newName: "Logs");

            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "Logs",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Logs",
                table: "Logs",
                column: "Id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Admin_AdminId",
                table: "Logs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Logs",
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

            migrationBuilder.RenameTable(
                name: "Logs",
                newName: "Log");

            migrationBuilder.AddColumn<int>(
                name: "ActivityLogId",
                table: "Admin",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ActivityLogId",
                table: "Log",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Log",
                table: "Log",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ActivityLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLog", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5046d7c1-edb9-4955-8449-1577542166cc", "82806a36-751e-4225-92ca-40d6f38c0ffe", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_Admin_ActivityLogId",
                table: "Admin",
                column: "ActivityLogId");

            migrationBuilder.CreateIndex(
                name: "IX_Log_ActivityLogId",
                table: "Log",
                column: "ActivityLogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Admin_ActivityLog_ActivityLogId",
                table: "Admin",
                column: "ActivityLogId",
                principalTable: "ActivityLog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Log_ActivityLog_ActivityLogId",
                table: "Log",
                column: "ActivityLogId",
                principalTable: "ActivityLog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
