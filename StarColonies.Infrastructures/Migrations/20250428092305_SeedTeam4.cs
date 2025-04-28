using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StarColonies.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class SeedTeam4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Avatar", "ConcurrencyStamp", "DateBirth", "Email", "EmailConfirmed", "Endurance", "IdProfession", "Level", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Strength", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "ID_COLON_1", 0, "avatar_1.png", "dfb5abc6-74a2-45c5-afcc-7e589eff1d5f", new DateTime(2005, 4, 28, 11, 23, 4, 818, DateTimeKind.Local).AddTicks(4107), "alex.striker@example.com", true, 2, 1, 0, false, null, "ALEX.STRIKER@EXAMPLE.COM", "ALEX STRIKER", "AQAAAAIAAYagAAAAELyDcFzyP+mB3Qd1BX4SHMMiZjGEc7kw7tzLUbHI7uCdIeK6YIfR0hR067yRomfWgg==", null, false, "8b2494eb-471e-4826-b050-d07f09ef2064", 5, false, "Alex Striker" },
                    { "ID_COLON_2", 0, "avatar_2.png", "ddf5deba-551e-422d-aeaa-aff8bc3ce5fc", new DateTime(2000, 4, 28, 11, 23, 4, 877, DateTimeKind.Local).AddTicks(7692), "mira.nova@example.com", true, 2, 2, 0, false, null, "MIRA.NOVA@EXAMPLE.COM", "MIRA NOVA", "AQAAAAIAAYagAAAAECpgpJYRlKdxKNJwZrSkaoBUexucDR/5GVph0EzGtAeb4MlFAlN04hPMsYsa5n3EHQ==", null, false, "451c57e2-9ca1-40ea-93b5-e5316f641835", 5, false, "Mira Nova" },
                    { "ID_COLON_3", 0, "avatar_3.png", "453e4722-6d5b-4513-b379-2561d0a3af23", new DateTime(1995, 4, 28, 11, 23, 4, 936, DateTimeKind.Local).AddTicks(5339), "elara.starfinder@example.com", true, 2, 3, 0, false, null, "ELARA.STARFINDER@EXAMPLE.COM", "ELARA STARFINDER", "AQAAAAIAAYagAAAAEB+znF3L0W3gyBV0y3hM0ouzE1yiULOqge/P8F1tgt70BGVUeU4YKYYbCaIdkDpmHw==", null, false, "87cf69ce-9d31-4232-9f93-87793ecb22e2", 5, false, "Elara Starfinder" },
                    { "ID_COLON_4", 0, "avatar_4.png", "f98a8782-2679-4c6b-9512-1aaf68140c37", new DateTime(1990, 4, 28, 11, 23, 4, 995, DateTimeKind.Local).AddTicks(1848), "jason.blades@example.com", true, 2, 4, 0, false, null, "JASON.BLADES@EXAMPLE.COM", "JASON BLADES", "AQAAAAIAAYagAAAAENZJ0Z1PzjGA5Rz3TuZ9GzLKQK7Y9pkKYLZiczTu3BoehEMdrsusGPt3K+9OaVZV3Q==", null, false, "8d06ec1e-503e-464b-b8da-7186a9094201", 5, false, "Jason Blades" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ID_COLON_1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ID_COLON_2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ID_COLON_3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ID_COLON_4");
        }
    }
}
