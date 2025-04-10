using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StarColonies.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class Profession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Profession",
                columns: new[] { "Id", "Description", "Icone", "Name" },
                values: new object[,]
                {
                    { 1, "Un ingénieur est une personne qui conçoit et construit des machines.", "engineer.png", "Ingénieur" },
                    { 2, "Un médecin est une personne qui soigne les maladies.", "doctor.png", "Médecin" },
                    { 3, "Un scientifique est une personne qui étudie la science.", "scientist.png", "Scientifique" },
                    { 4, "Un soldat est une personne qui combat pour son pays.", "soldier.png", "Soldat" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Profession",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Profession",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Profession",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Profession",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
