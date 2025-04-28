using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarColonies.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class SeedTeam5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ID_COLON_1",
                columns: new[] { "ConcurrencyStamp", "DateBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8a6b9083-8808-4207-9c16-8c592bd9417b", new DateTime(2005, 4, 28, 11, 23, 47, 712, DateTimeKind.Local).AddTicks(4078), "AQAAAAIAAYagAAAAEAg1BM4PJaCLPYpsKXzmP1F82mijxYOYbUgtmrwGuU2MyeDLdycNSEMVRP8eQZhFiA==", "d492c8b1-55af-4151-a3b5-35ed7eae4555" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ID_COLON_2",
                columns: new[] { "ConcurrencyStamp", "DateBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "15ffd771-3bb4-488e-9bc0-3c3d35980668", new DateTime(2000, 4, 28, 11, 23, 47, 769, DateTimeKind.Local).AddTicks(6210), "AQAAAAIAAYagAAAAEHL6dPf6AJ5FPM1NP5z+zCt/VcGPS7G97kXzeGDxxiPu8W65J4d1nVGdovVsAQLcTA==", "5f5250df-f281-4056-be33-6458b287c46d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ID_COLON_3",
                columns: new[] { "ConcurrencyStamp", "DateBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b8909ca2-af2b-4256-bf78-be4a86a8d8c4", new DateTime(1995, 4, 28, 11, 23, 47, 833, DateTimeKind.Local).AddTicks(6684), "AQAAAAIAAYagAAAAELEMRD2WxsV1YnhkZsNHpe+uzWzj9xFlpmd5Zo8u79Q6rtclBynGmXrDTCcidCStvQ==", "8c7886e7-eacc-4bb7-a827-499beb4966de" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ID_COLON_4",
                columns: new[] { "ConcurrencyStamp", "DateBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "349cbe68-d64a-4b1c-8c0d-d6f7b50eabd8", new DateTime(1990, 4, 28, 11, 23, 47, 896, DateTimeKind.Local).AddTicks(4129), "AQAAAAIAAYagAAAAEJXm/SABJg3a6BjdFF/ceEitV2ZCnGmY5AgCnEf/53rrUY+laPSoiwgPSQu2VSS5XA==", "6010b6eb-7e8c-4619-a0bc-4fe738da73a6" });

            migrationBuilder.InsertData(
                table: "Team",
                columns: new[] { "Id", "Baniere", "IdColonCreator", "Logo", "Name" },
                values: new object[] { -1, "default_baniere.png", "ID_COLON_1", "default_logo.png", "Team du PDF" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Team",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ID_COLON_1",
                columns: new[] { "ConcurrencyStamp", "DateBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dfb5abc6-74a2-45c5-afcc-7e589eff1d5f", new DateTime(2005, 4, 28, 11, 23, 4, 818, DateTimeKind.Local).AddTicks(4107), "AQAAAAIAAYagAAAAELyDcFzyP+mB3Qd1BX4SHMMiZjGEc7kw7tzLUbHI7uCdIeK6YIfR0hR067yRomfWgg==", "8b2494eb-471e-4826-b050-d07f09ef2064" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ID_COLON_2",
                columns: new[] { "ConcurrencyStamp", "DateBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ddf5deba-551e-422d-aeaa-aff8bc3ce5fc", new DateTime(2000, 4, 28, 11, 23, 4, 877, DateTimeKind.Local).AddTicks(7692), "AQAAAAIAAYagAAAAECpgpJYRlKdxKNJwZrSkaoBUexucDR/5GVph0EzGtAeb4MlFAlN04hPMsYsa5n3EHQ==", "451c57e2-9ca1-40ea-93b5-e5316f641835" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ID_COLON_3",
                columns: new[] { "ConcurrencyStamp", "DateBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "453e4722-6d5b-4513-b379-2561d0a3af23", new DateTime(1995, 4, 28, 11, 23, 4, 936, DateTimeKind.Local).AddTicks(5339), "AQAAAAIAAYagAAAAEB+znF3L0W3gyBV0y3hM0ouzE1yiULOqge/P8F1tgt70BGVUeU4YKYYbCaIdkDpmHw==", "87cf69ce-9d31-4232-9f93-87793ecb22e2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ID_COLON_4",
                columns: new[] { "ConcurrencyStamp", "DateBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f98a8782-2679-4c6b-9512-1aaf68140c37", new DateTime(1990, 4, 28, 11, 23, 4, 995, DateTimeKind.Local).AddTicks(1848), "AQAAAAIAAYagAAAAENZJ0Z1PzjGA5Rz3TuZ9GzLKQK7Y9pkKYLZiczTu3BoehEMdrsusGPt3K+9OaVZV3Q==", "8d06ec1e-503e-464b-b8da-7186a9094201" });
        }
    }
}
