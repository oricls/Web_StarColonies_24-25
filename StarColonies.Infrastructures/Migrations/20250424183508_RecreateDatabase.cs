using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarColonies.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class RecreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
{
    //Triche
    migrationBuilder.Sql(@"
        IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'BonusResource')
        BEGIN
            CREATE TABLE BonusResource (
                Id int IDENTITY(1,1) PRIMARY KEY,
                BonusId int NOT NULL,
                ResourceId int NOT NULL,
                Quantite int NOT NULL,
                CONSTRAINT FK_BonusResource_Bonus FOREIGN KEY (BonusId) REFERENCES Bonus(Id),
                CONSTRAINT FK_BonusResource_Resource FOREIGN KEY (ResourceId) REFERENCES Resource(Id)
            );
            
            CREATE UNIQUE INDEX IX_BonusResource_BonusId_ResourceId ON BonusResource(BonusId, ResourceId);
        END
        ELSE
        BEGIN
            -- Modification d'une table existante (ne sera pas exécuté si la table a été supprimée)
            ALTER TABLE BonusResource DROP CONSTRAINT PK_BonusResource;
            ALTER TABLE BonusResource ADD CONSTRAINT PK_BonusResource PRIMARY KEY (Id);
            CREATE UNIQUE INDEX IX_BonusResource_BonusId_ResourceId ON BonusResource(BonusId, ResourceId);
        END
    ");

    // Création des tables pour les transactions
    migrationBuilder.CreateTable(
        name: "BonusTransaction",
        columns: table => new
        {
            Id = table.Column<int>(type: "int", nullable: false)
                .Annotation("SqlServer:Identity", "1, 1"),
            ColonId = table.Column<string>(type: "nvarchar(450)", nullable: false),
            BonusId = table.Column<int>(type: "int", nullable: false),
            TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_BonusTransaction", x => x.Id);
            table.ForeignKey(
                name: "FK_BonusTransaction_AspNetUsers_ColonId",
                column: x => x.ColonId,
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                name: "FK_BonusTransaction_Bonus_BonusId",
                column: x => x.BonusId,
                principalTable: "Bonus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        });

    migrationBuilder.CreateTable(
        name: "BonusTransactionResource",
        columns: table => new
        {
            Id = table.Column<int>(type: "int", nullable: false)
                .Annotation("SqlServer:Identity", "1, 1"),
            TransactionId = table.Column<int>(type: "int", nullable: false),
            BonusResourceId = table.Column<int>(type: "int", nullable: false),
            Quantite = table.Column<int>(type: "int", nullable: false)
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_BonusTransactionResource", x => x.Id);
            table.ForeignKey(
                name: "FK_BonusTransactionResource_BonusResource_BonusResourceId",
                column: x => x.BonusResourceId,
                principalTable: "BonusResource",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            table.ForeignKey(
                name: "FK_BonusTransactionResource_BonusTransaction_TransactionId",
                column: x => x.TransactionId,
                principalTable: "BonusTransaction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        });

    migrationBuilder.CreateIndex(
        name: "IX_BonusTransaction_BonusId",
        table: "BonusTransaction",
        column: "BonusId");

    migrationBuilder.CreateIndex(
        name: "IX_BonusTransaction_ColonId",
        table: "BonusTransaction",
        column: "ColonId");

    migrationBuilder.CreateIndex(
        name: "IX_BonusTransactionResource_BonusResourceId",
        table: "BonusTransactionResource",
        column: "BonusResourceId");

    migrationBuilder.CreateIndex(
        name: "IX_BonusTransactionResource_TransactionId",
        table: "BonusTransactionResource",
        column: "TransactionId");
}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BonusTransactionResource");

            migrationBuilder.DropTable(
                name: "BonusTransaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BonusResource",
                table: "BonusResource");

            migrationBuilder.DropIndex(
                name: "IX_BonusResource_BonusId_ResourceId",
                table: "BonusResource");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "BonusResource",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BonusResource",
                table: "BonusResource",
                columns: new[] { "BonusId", "ResourceId" });
        }
    }
}
