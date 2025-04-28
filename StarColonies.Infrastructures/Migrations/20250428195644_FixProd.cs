using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StarColonies.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class FixProd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ID_COLON_1",
                columns: new[] { "ConcurrencyStamp", "DateBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f4dad9a2-71c4-41b8-a603-66c092944e33", new DateTime(2005, 4, 28, 21, 56, 44, 122, DateTimeKind.Local).AddTicks(1460), "AQAAAAIAAYagAAAAEHcULfWHGZGp8r0WlXuJXuCAlmRnIu2pae41jhWEABKZPWyeA2sx2A2EpXrKJTB6vQ==", "7c75ff34-162a-4eb4-a90d-fec9894dd117" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ID_COLON_10",
                columns: new[] { "Avatar", "ConcurrencyStamp", "DateBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "avatar_5.png", "04ed5d48-d8da-4c94-84fc-56bb44774e5b", new DateTime(1998, 4, 28, 21, 56, 44, 406, DateTimeKind.Local).AddTicks(9790), "AQAAAAIAAYagAAAAEMoP6Qo7gEjH+DUmD94ioOpVmMX890Cjo/pzGhMWo9A7yqzCo7svpGnhvLlR9Nk29w==", "7ffcb5e3-b2a8-4342-9d64-6e220a385e6b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ID_COLON_2",
                columns: new[] { "ConcurrencyStamp", "DateBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "43ccd8ab-6cc2-47d4-be8a-b5c5b3196fe1", new DateTime(2000, 4, 28, 21, 56, 44, 154, DateTimeKind.Local).AddTicks(6210), "AQAAAAIAAYagAAAAEH26HE0HGWNQDjKgw+xygL1AAFYP9Xi7clmjQJRc1+9Zs9itB7P09BhFgyem0N1o4Q==", "18f94a6d-e0d2-4afc-ae84-552ef508d81c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ID_COLON_3",
                columns: new[] { "ConcurrencyStamp", "DateBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d310943a-1b01-499b-8096-3b4ddda7a2a7", new DateTime(1995, 4, 28, 21, 56, 44, 186, DateTimeKind.Local).AddTicks(7260), "AQAAAAIAAYagAAAAEMDLmSuc71QjxFxe//y5kqLo8M3mdWIsvkpJPCQibXBTzYR/uBUPEmEsk1exV9JTOg==", "2ef741c5-219d-4f58-a869-05723a48be85" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ID_COLON_4",
                columns: new[] { "ConcurrencyStamp", "DateBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d7832c53-0fb0-4ce0-aed3-d7d252988c94", new DateTime(1990, 4, 28, 21, 56, 44, 217, DateTimeKind.Local).AddTicks(7330), "AQAAAAIAAYagAAAAEGf1vCNU/7PecUyD+cdd528rTfx4q6IhUlFvIAkZIeEjvv2Gfj6Ek0XWNY1Pv2JXug==", "465fffa1-6d6a-4e37-a25e-99bb2c4717b9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ID_COLON_5",
                columns: new[] { "ConcurrencyStamp", "DateBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9ef16d3c-b9cd-4337-a65f-44e48a6ba45e", new DateTime(1997, 4, 28, 21, 56, 44, 249, DateTimeKind.Local).AddTicks(5550), "AQAAAAIAAYagAAAAEAYsQX0jRis2XrGs61SQcNFOGvlceCjmLC68WMMSBPpo9mNdQJ+8H5cAR3ViRW9N7g==", "eb0b29bc-37b2-47f7-b3eb-1dad8650fdd7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ID_COLON_6",
                columns: new[] { "Avatar", "ConcurrencyStamp", "DateBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "avatar_2.png", "4d78dc86-f2c8-47ac-aea8-4a1fd83a15cd", new DateTime(1993, 4, 28, 21, 56, 44, 281, DateTimeKind.Local).AddTicks(8430), "AQAAAAIAAYagAAAAEHZ23Iok3KkE8QC4uvhAdl2HMJEiE/eI88BAlwQStP6/YUkAVnPPnyOAzpgeO3p4HA==", "bce5d473-2852-47da-953e-ca02d070148c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ID_COLON_7",
                columns: new[] { "Avatar", "ConcurrencyStamp", "DateBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "avatar_1.png", "3f5c530c-b952-4c3d-911f-460ff16ae60c", new DateTime(1999, 4, 28, 21, 56, 44, 313, DateTimeKind.Local).AddTicks(4650), "AQAAAAIAAYagAAAAEKGLM7kSmyAv0sIUR6YU3chFkzPOeIHe4A0QW9w3NlmdF4dX+Nt496S64J8NWI40xA==", "50c97433-1cb2-466c-bb67-f14be562558f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ID_COLON_8",
                columns: new[] { "Avatar", "ConcurrencyStamp", "DateBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "avatar_3.png", "90aad3f0-6662-4589-a781-19e388233a10", new DateTime(1996, 4, 28, 21, 56, 44, 344, DateTimeKind.Local).AddTicks(5690), "AQAAAAIAAYagAAAAECaNjOe7xALLeGmDT9sQZNLJVsBOlnSdMxZsybzXpPqbpe50MyYm8YgKEPtJtMvTuA==", "d650e79c-2160-4913-9e05-56f975ea580f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ID_COLON_9",
                columns: new[] { "Avatar", "ConcurrencyStamp", "DateBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "avatar_4.png", "796fc595-b106-404a-8f57-b5f5506587a4", new DateTime(2001, 4, 28, 21, 56, 44, 375, DateTimeKind.Local).AddTicks(8360), "AQAAAAIAAYagAAAAEGf8t2HMWHI5DYGQoGHxp7dIHQTiGKEXC1eGnx/6OjMZ2gY5/NQqmbxFuavidR6IHQ==", "dd781379-9c20-436f-8406-13f476c1a529" });

            migrationBuilder.UpdateData(
                table: "Bonus",
                keyColumn: "Id",
                keyValue: 1,
                column: "IconUrl",
                value: "big_potion.png");

            migrationBuilder.UpdateData(
                table: "Bonus",
                keyColumn: "Id",
                keyValue: 2,
                column: "IconUrl",
                value: "soldat.png");

            migrationBuilder.UpdateData(
                table: "Bonus",
                keyColumn: "Id",
                keyValue: 3,
                column: "IconUrl",
                value: "big_potion.png");

            migrationBuilder.UpdateData(
                table: "Bonus",
                keyColumn: "Id",
                keyValue: 4,
                column: "IconUrl",
                value: "midas.png");

            migrationBuilder.UpdateData(
                table: "Bonus",
                keyColumn: "Id",
                keyValue: 5,
                column: "IconUrl",
                value: "life.png");

            migrationBuilder.UpdateData(
                table: "Bonus",
                keyColumn: "Id",
                keyValue: 6,
                column: "IconUrl",
                value: "cheat.png");

            migrationBuilder.InsertData(
                table: "BonusResource",
                columns: new[] { "Id", "BonusId", "Quantite", "ResourceId" },
                values: new object[,]
                {
                    { 1, 1, 5, 1 },
                    { 2, 1, 3, 11 },
                    { 3, 1, 2, 3 },
                    { 4, 2, 1, 6 },
                    { 5, 2, 2, 18 },
                    { 6, 3, 4, 4 },
                    { 7, 3, 7, 14 },
                    { 8, 3, 1, 13 },
                    { 9, 4, 3, 12 },
                    { 10, 4, 1, 17 },
                    { 11, 5, 5, 20 },
                    { 12, 5, 3, 2 },
                    { 13, 5, 8, 15 },
                    { 14, 6, 2, 7 },
                    { 15, 6, 10, 16 },
                    { 16, 6, 4, 19 }
                });

            migrationBuilder.UpdateData(
                table: "Mission",
                keyColumn: "Id",
                keyValue: 1,
                column: "Image",
                value: "base.png");

            migrationBuilder.UpdateData(
                table: "Mission",
                keyColumn: "Id",
                keyValue: 2,
                column: "Image",
                value: "hunt.png");

            migrationBuilder.UpdateData(
                table: "Mission",
                keyColumn: "Id",
                keyValue: 3,
                column: "Image",
                value: "save.png");

            migrationBuilder.UpdateData(
                table: "Mission",
                keyColumn: "Id",
                keyValue: 4,
                column: "Image",
                value: "hegemon.png");

            migrationBuilder.UpdateData(
                table: "Mission",
                keyColumn: "Id",
                keyValue: 5,
                column: "Image",
                value: "ruines.png");

            migrationBuilder.UpdateData(
                table: "TypeBestiaire",
                keyColumn: "Id",
                keyValue: 1,
                column: "Avatar",
                value: "robot.png");

            migrationBuilder.UpdateData(
                table: "TypeBestiaire",
                keyColumn: "Id",
                keyValue: 2,
                column: "Avatar",
                value: "naturelle.png");

            migrationBuilder.UpdateData(
                table: "TypeBestiaire",
                keyColumn: "Id",
                keyValue: 3,
                column: "Avatar",
                value: "extraterrestre.png");

            migrationBuilder.UpdateData(
                table: "TypeBestiaire",
                keyColumn: "Id",
                keyValue: 4,
                column: "Avatar",
                value: "paranormal.png");

            migrationBuilder.UpdateData(
                table: "TypeBestiaire",
                keyColumn: "Id",
                keyValue: 5,
                column: "Avatar",
                value: "animal.png");

            migrationBuilder.UpdateData(
                table: "TypeBestiaire",
                keyColumn: "Id",
                keyValue: 6,
                column: "Avatar",
                value: "experience.png");

            migrationBuilder.UpdateData(
                table: "TypeBestiaire",
                keyColumn: "Id",
                keyValue: 7,
                column: "Avatar",
                value: "humanoide.png");

            migrationBuilder.UpdateData(
                table: "TypeResource",
                keyColumn: "Id",
                keyValue: 1,
                column: "Icon",
                value: "materiau.png");

            migrationBuilder.UpdateData(
                table: "TypeResource",
                keyColumn: "Id",
                keyValue: 2,
                column: "Icon",
                value: "technologie.png");

            migrationBuilder.UpdateData(
                table: "TypeResource",
                keyColumn: "Id",
                keyValue: 3,
                column: "Icon",
                value: "artefact.png");

            migrationBuilder.UpdateData(
                table: "TypeResource",
                keyColumn: "Id",
                keyValue: 4,
                column: "Icon",
                value: "connaissance.png");

            migrationBuilder.UpdateData(
                table: "TypeResource",
                keyColumn: "Id",
                keyValue: 5,
                column: "Icon",
                value: "consomable.png");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BonusResource",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BonusResource",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BonusResource",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BonusResource",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "BonusResource",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "BonusResource",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "BonusResource",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "BonusResource",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "BonusResource",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "BonusResource",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "BonusResource",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "BonusResource",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "BonusResource",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "BonusResource",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "BonusResource",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "BonusResource",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ID_COLON_1",
                columns: new[] { "ConcurrencyStamp", "DateBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "03ca1f88-b451-4304-a0b6-4cbac1f31a68", new DateTime(2005, 4, 28, 20, 26, 59, 177, DateTimeKind.Local).AddTicks(7120), "AQAAAAIAAYagAAAAEEVUetnBp7FeFM6yy0sT4Uq2ss81sXrB30s0MzgXzwAar5RP4+/aWYQ54EVMa8ifkQ==", "faa7d029-d798-4ad7-8b15-08dff23ce8ea" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ID_COLON_10",
                columns: new[] { "Avatar", "ConcurrencyStamp", "DateBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "avatar_10.png", "447a4065-f665-49f3-a235-97197257fca9", new DateTime(1998, 4, 28, 20, 26, 59, 463, DateTimeKind.Local).AddTicks(9370), "AQAAAAIAAYagAAAAEHjy+ycaLif5S9WbZ/OMU42k2T3ljsP7aYUmvLhK+98xAwWQhpFrwOcjMUyo8qdp+g==", "98a9f97a-6976-48ae-b01d-fd318e454d59" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ID_COLON_2",
                columns: new[] { "ConcurrencyStamp", "DateBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b14c4d7c-cd5b-4ace-b409-280d39c9b4fb", new DateTime(2000, 4, 28, 20, 26, 59, 210, DateTimeKind.Local).AddTicks(950), "AQAAAAIAAYagAAAAEJ8mC0iBVruo5Atlav7cE4SjNsLW4QERZVo2YdRwJsjq2NOhadx2Mht1X4zB/zyS/g==", "d68c94e1-a614-4bce-8872-86e7480719e6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ID_COLON_3",
                columns: new[] { "ConcurrencyStamp", "DateBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "489c3fa9-a579-4809-a668-bac2cc7a9dd1", new DateTime(1995, 4, 28, 20, 26, 59, 241, DateTimeKind.Local).AddTicks(9230), "AQAAAAIAAYagAAAAEJndSlzy0pet9dhD2xPcnPm35q4AuuRIvIQIpY9G5sTqEsu0zrW+Bs5OEm4Biw/RNQ==", "1e4dbb19-e83b-4d38-8197-d1eef281140f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ID_COLON_4",
                columns: new[] { "ConcurrencyStamp", "DateBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a99828c7-45b0-4af6-bdee-c269bc5343f8", new DateTime(1990, 4, 28, 20, 26, 59, 274, DateTimeKind.Local).AddTicks(2490), "AQAAAAIAAYagAAAAECE3ba2YtIk46OpipBswVr21fWlN2wXvP2T4s5cfoMnmSsxsG8a7HbQ+3NS6A3gioQ==", "6a966e87-e0b9-44fd-931e-59b9a0916e39" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ID_COLON_5",
                columns: new[] { "ConcurrencyStamp", "DateBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "23a6b1d6-473e-416b-90f0-4f485a604cea", new DateTime(1997, 4, 28, 20, 26, 59, 306, DateTimeKind.Local).AddTicks(5210), "AQAAAAIAAYagAAAAEOynJyOjXGYV0i+/UIeGc09roK//l+f8zyh/keFViDh3qP8ww/vwuyDopx5YwBN5KA==", "e95bd084-d6d5-4f97-addc-bf0f5d951a97" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ID_COLON_6",
                columns: new[] { "Avatar", "ConcurrencyStamp", "DateBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "avatar_6.png", "185fc2bb-4b9a-438b-aa0b-4cc237669027", new DateTime(1993, 4, 28, 20, 26, 59, 337, DateTimeKind.Local).AddTicks(3110), "AQAAAAIAAYagAAAAEGcDpKQFA9C8c+Or9q2OylSFhUU8skRPTaGxpq2BExtHxjaBApRRVULNbsneXhaqjA==", "96596525-997e-4d7b-8340-dc095755e7c5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ID_COLON_7",
                columns: new[] { "Avatar", "ConcurrencyStamp", "DateBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "avatar_7.png", "ac81e0a4-1e87-4344-a76a-9fa6ee39ce15", new DateTime(1999, 4, 28, 20, 26, 59, 368, DateTimeKind.Local).AddTicks(2120), "AQAAAAIAAYagAAAAEGmYHTt55WXfb6Xstvg2hY0MQZtLYe8JYIJJO53oVpmsWolHnG/WH+BRr/Z7vmIZpQ==", "e6586aaf-3cb6-4739-b321-11c7075c3294" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ID_COLON_8",
                columns: new[] { "Avatar", "ConcurrencyStamp", "DateBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "avatar_8.png", "c4a1adde-fe2d-40b7-b88b-e68082620eda", new DateTime(1996, 4, 28, 20, 26, 59, 399, DateTimeKind.Local).AddTicks(5170), "AQAAAAIAAYagAAAAEC9DIZz5bqsI5jE4d7k8p5K/6+JXpb9hztJgDL/krYznGD6wmNDzB++RXM1hr4xD6Q==", "1bf6bd08-0ab5-4e2b-bdae-5fce73c092ff" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ID_COLON_9",
                columns: new[] { "Avatar", "ConcurrencyStamp", "DateBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "avatar_9.png", "7ae78c90-2a94-4b6b-8e48-d0b578d30f9f", new DateTime(2001, 4, 28, 20, 26, 59, 431, DateTimeKind.Local).AddTicks(5320), "AQAAAAIAAYagAAAAEMNnUHeSdct8RlcS1ncenrbTy8/WSSyeLxJDe8ogOYMqThPfF/kA1l1GG1YzMh3T2A==", "f0df683f-7295-410c-908a-95ec7b13d2fd" });

            migrationBuilder.UpdateData(
                table: "Bonus",
                keyColumn: "Id",
                keyValue: 1,
                column: "IconUrl",
                value: "assets/icons/potion.png");

            migrationBuilder.UpdateData(
                table: "Bonus",
                keyColumn: "Id",
                keyValue: 2,
                column: "IconUrl",
                value: "assets/icons/potion.png");

            migrationBuilder.UpdateData(
                table: "Bonus",
                keyColumn: "Id",
                keyValue: 3,
                column: "IconUrl",
                value: "assets/icons/potion.png");

            migrationBuilder.UpdateData(
                table: "Bonus",
                keyColumn: "Id",
                keyValue: 4,
                column: "IconUrl",
                value: "assets/icons/potion.png");

            migrationBuilder.UpdateData(
                table: "Bonus",
                keyColumn: "Id",
                keyValue: 5,
                column: "IconUrl",
                value: "assets/icons/potion.png");

            migrationBuilder.UpdateData(
                table: "Bonus",
                keyColumn: "Id",
                keyValue: 6,
                column: "IconUrl",
                value: "assets/icons/potion.png");

            migrationBuilder.UpdateData(
                table: "Mission",
                keyColumn: "Id",
                keyValue: 1,
                column: "Image",
                value: "base_abandonnee.jpg");

            migrationBuilder.UpdateData(
                table: "Mission",
                keyColumn: "Id",
                keyValue: 2,
                column: "Image",
                value: "leviathan_abysses.jpg");

            migrationBuilder.UpdateData(
                table: "Mission",
                keyColumn: "Id",
                keyValue: 3,
                column: "Image",
                value: "quarantaine_zone.jpg");

            migrationBuilder.UpdateData(
                table: "Mission",
                keyColumn: "Id",
                keyValue: 4,
                column: "Image",
                value: "hegemon_reactor.jpg");

            migrationBuilder.UpdateData(
                table: "Mission",
                keyColumn: "Id",
                keyValue: 5,
                column: "Image",
                value: "ruines_aliennes.jpg");

            migrationBuilder.UpdateData(
                table: "TypeBestiaire",
                keyColumn: "Id",
                keyValue: 1,
                column: "Avatar",
                value: "Avatar1.png");

            migrationBuilder.UpdateData(
                table: "TypeBestiaire",
                keyColumn: "Id",
                keyValue: 2,
                column: "Avatar",
                value: "icons/naturelle.png");

            migrationBuilder.UpdateData(
                table: "TypeBestiaire",
                keyColumn: "Id",
                keyValue: 3,
                column: "Avatar",
                value: "icons/extraterrestre.png");

            migrationBuilder.UpdateData(
                table: "TypeBestiaire",
                keyColumn: "Id",
                keyValue: 4,
                column: "Avatar",
                value: "icons/paranormal.png");

            migrationBuilder.UpdateData(
                table: "TypeBestiaire",
                keyColumn: "Id",
                keyValue: 5,
                column: "Avatar",
                value: "icons/animal.png");

            migrationBuilder.UpdateData(
                table: "TypeBestiaire",
                keyColumn: "Id",
                keyValue: 6,
                column: "Avatar",
                value: "icons/experience.png");

            migrationBuilder.UpdateData(
                table: "TypeBestiaire",
                keyColumn: "Id",
                keyValue: 7,
                column: "Avatar",
                value: "icons/humanoide.png");

            migrationBuilder.UpdateData(
                table: "TypeResource",
                keyColumn: "Id",
                keyValue: 1,
                column: "Icon",
                value: "icons/materiau.png");

            migrationBuilder.UpdateData(
                table: "TypeResource",
                keyColumn: "Id",
                keyValue: 2,
                column: "Icon",
                value: "icons/technologie.png");

            migrationBuilder.UpdateData(
                table: "TypeResource",
                keyColumn: "Id",
                keyValue: 3,
                column: "Icon",
                value: "icons/artefact.png");

            migrationBuilder.UpdateData(
                table: "TypeResource",
                keyColumn: "Id",
                keyValue: 4,
                column: "Icon",
                value: "icons/connaissance.png");

            migrationBuilder.UpdateData(
                table: "TypeResource",
                keyColumn: "Id",
                keyValue: 5,
                column: "Icon",
                value: "icons/consomable.png");
        }
    }
}
