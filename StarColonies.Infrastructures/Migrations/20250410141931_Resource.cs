using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StarColonies.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class Resource : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Profession",
                keyColumn: "Id",
                keyValue: 1,
                column: "Icone",
                value: "avatars/engineer.png");

            migrationBuilder.UpdateData(
                table: "Profession",
                keyColumn: "Id",
                keyValue: 2,
                column: "Icone",
                value: "avatars/doctor.png");

            migrationBuilder.UpdateData(
                table: "Profession",
                keyColumn: "Id",
                keyValue: 3,
                column: "Icone",
                value: "avatars/scientist.png");

            migrationBuilder.UpdateData(
                table: "Profession",
                keyColumn: "Id",
                keyValue: 4,
                column: "Icone",
                value: "avatars/soldier.png");

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

            migrationBuilder.InsertData(
                table: "TypeResource",
                columns: new[] { "Id", "Description", "Icon", "Name" },
                values: new object[,]
                {
                    { 1, "Matériau de construction", "icons/materiau.png", "Matériau" },
                    { 2, "Technologie avancée", "icons/technologie.png", "Technologie" },
                    { 3, "Artefact ancien", "icons/artefact.png", "Artefact" },
                    { 4, "Connaissance avancée", "icons/connaissance.png", "Connaissance" },
                    { 5, "Consomable de base", "icons/consomable.png", "Consomable" }
                });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "Id", "Description", "IdTypeResource", "Name" },
                values: new object[,]
                {
                    { 1, "Matériau de protection léger", 1, "Elindage léger" },
                    { 2, "Technologie de stockage d'énergie", 2, "Batterie compacte" },
                    { 3, "Composant technologique avancé", 2, "Module quantique" },
                    { 4, "Matériau haute résistance", 1, "Nanofibres" },
                    { 5, "Matériau de protection renforcé", 1, "Elindage renforcé" },
                    { 6, "Arme technologique", 2, "Mitraillette" },
                    { 7, "Artefact mystérieux", 3, "Gros crâne d'Alex" },
                    { 8, "Matériau suspect", 1, "Residus louches" },
                    { 9, "Substance consommable dégradée", 5, "Vinade avariée" },
                    { 10, "Matériau provenant de dents animales", 1, "Crocs tranchants" },
                    { 11, "Substance consommable bioluminescente", 5, "Sang fluorescent" },
                    { 12, "Connaissance génétique", 4, "ADN cryptoïen" },
                    { 13, "Artefact de protection massive", 3, "Enorme casque" },
                    { 14, "Boisson consommable", 5, "Petite bière" },
                    { 15, "Matériau de protection naturelle", 1, "Ecaille solide" },
                    { 16, "Substance consommable dangereuse", 5, "Venin mortel" },
                    { 17, "Artefact énigmatique", 3, "Cœur de l'Hégémon" },
                    { 18, "Connaissance stratégique", 4, "Plan d'attaque" },
                    { 19, "Matériau spectral", 1, "Membrane fantomique" },
                    { 20, "Connaissance extrasensorielle", 4, "Clairevoyance" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Resource",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Resource",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Resource",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Resource",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Resource",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Resource",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Resource",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Resource",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Resource",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Resource",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Resource",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Resource",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Resource",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Resource",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Resource",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Resource",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Resource",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Resource",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Resource",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Resource",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "TypeResource",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TypeResource",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TypeResource",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TypeResource",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TypeResource",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Profession",
                keyColumn: "Id",
                keyValue: 1,
                column: "Icone",
                value: "engineer.png");

            migrationBuilder.UpdateData(
                table: "Profession",
                keyColumn: "Id",
                keyValue: 2,
                column: "Icone",
                value: "doctor.png");

            migrationBuilder.UpdateData(
                table: "Profession",
                keyColumn: "Id",
                keyValue: 3,
                column: "Icone",
                value: "scientist.png");

            migrationBuilder.UpdateData(
                table: "Profession",
                keyColumn: "Id",
                keyValue: 4,
                column: "Icone",
                value: "soldier.png");

            migrationBuilder.UpdateData(
                table: "TypeBestiaire",
                keyColumn: "Id",
                keyValue: 2,
                column: "Avatar",
                value: "Avatar2.png");

            migrationBuilder.UpdateData(
                table: "TypeBestiaire",
                keyColumn: "Id",
                keyValue: 3,
                column: "Avatar",
                value: "Avatar3.png");

            migrationBuilder.UpdateData(
                table: "TypeBestiaire",
                keyColumn: "Id",
                keyValue: 4,
                column: "Avatar",
                value: "Avatar4.png");

            migrationBuilder.UpdateData(
                table: "TypeBestiaire",
                keyColumn: "Id",
                keyValue: 5,
                column: "Avatar",
                value: "Avatar5.png");

            migrationBuilder.UpdateData(
                table: "TypeBestiaire",
                keyColumn: "Id",
                keyValue: 6,
                column: "Avatar",
                value: "Avatar6.png");

            migrationBuilder.UpdateData(
                table: "TypeBestiaire",
                keyColumn: "Id",
                keyValue: 7,
                column: "Avatar",
                value: "Avatar7.png");
        }
    }
}
