using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarColonies.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class ChangeResultatMission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Issue",
                table: "ResultatMission");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "ResultatMission",
                type: "datetime2",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<double>(
                name: "IssueEndurance",
                table: "ResultatMission",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "IssueStrength",
                table: "ResultatMission",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IssueEndurance",
                table: "ResultatMission");

            migrationBuilder.DropColumn(
                name: "IssueStrength",
                table: "ResultatMission");

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "ResultatMission",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "Issue",
                table: "ResultatMission",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
