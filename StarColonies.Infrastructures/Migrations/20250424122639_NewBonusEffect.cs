using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarColonies.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class NewBonusEffect : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bonus",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "EffectTypeId", "Name" },
                values: new object[] { "Augmente l'expérience gagnée et permet de gagner un niveau supplémentaire à chaque mission", 5, "Parchemin de monsieur Swinnen" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bonus",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "EffectTypeId", "Name" },
                values: new object[] { "Chaque colon se voit octroyer une vie supplémentaire (endurance ×2)", 2, "Seconde chance" });
        }
    }
}
