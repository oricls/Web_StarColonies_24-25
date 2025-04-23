using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarColonies.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class AddBonusEffectTypeAndIcon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EffectTypeId",
                table: "Bonus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "IconUrl",
                table: "Bonus",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Bonus",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DureeParDefaut", "EffectTypeId", "IconUrl" },
                values: new object[] { new TimeSpan(0, 0, 5, 0, 0), 1, "assets/icons/potion.png" });

            migrationBuilder.UpdateData(
                table: "Bonus",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "EffectTypeId", "IconUrl", "Name" },
                values: new object[] { "Investit un soldat supplémentaire pour les prochaines missions", 3, "assets/icons/potion.png", "Coup de pouce" });

            migrationBuilder.UpdateData(
                table: "Bonus",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DureeParDefaut", "EffectTypeId", "IconUrl" },
                values: new object[] { new TimeSpan(0, 0, 5, 0, 0), 2, "assets/icons/potion.png" });

            migrationBuilder.UpdateData(
                table: "Bonus",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "DureeParDefaut", "EffectTypeId", "IconUrl" },
                values: new object[] { "Double le nombre de ressources obtenues", new TimeSpan(0, 0, 1, 0, 0), 4, "assets/icons/potion.png" });

            migrationBuilder.UpdateData(
                table: "Bonus",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DureeParDefaut", "EffectTypeId", "IconUrl" },
                values: new object[] { new TimeSpan(0, 0, 20, 0, 0), 2, "assets/icons/potion.png" });

            migrationBuilder.UpdateData(
                table: "Bonus",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "IconUrl", "Name" },
                values: new object[] { "assets/icons/potion.png", "L'item trop cheatés" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EffectTypeId",
                table: "Bonus");

            migrationBuilder.DropColumn(
                name: "IconUrl",
                table: "Bonus");

            migrationBuilder.UpdateData(
                table: "Bonus",
                keyColumn: "Id",
                keyValue: 1,
                column: "DureeParDefaut",
                value: new TimeSpan(0, 0, 20, 0, 0));

            migrationBuilder.UpdateData(
                table: "Bonus",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Investit un soldat supplémentaire pour les 3 prochaines missions", "Equipe de pouce" });

            migrationBuilder.UpdateData(
                table: "Bonus",
                keyColumn: "Id",
                keyValue: 3,
                column: "DureeParDefaut",
                value: new TimeSpan(0, 0, 20, 0, 0));

            migrationBuilder.UpdateData(
                table: "Bonus",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "DureeParDefaut" },
                values: new object[] { "Double le nombre de ressources obtenues pour 1 mission", new TimeSpan(0, 0, 10, 0, 0) });

            migrationBuilder.UpdateData(
                table: "Bonus",
                keyColumn: "Id",
                keyValue: 5,
                column: "DureeParDefaut",
                value: new TimeSpan(0, 0, 10, 0, 0));

            migrationBuilder.UpdateData(
                table: "Bonus",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Litem trop cheats");
        }
    }
}
