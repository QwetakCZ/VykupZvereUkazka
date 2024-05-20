using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuRa_Vykup.Data.Migrations
{
    /// <inheritdoc />
    public partial class ceniky : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Dodavatel",
                table: "Dodavatele",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<int>(
                name: "CenikId",
                table: "Dodavatele",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Ceniky",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nazev = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SrnciI = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SrnciII = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SrnciIII = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DanekI = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DanekII = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DanekIII = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    JelenI = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    JelenII = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    JelenIII = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DivocekI = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DivocekII = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DivocekIII = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SikaI = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SikaII = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SikaIII = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MuflonI = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MuflonII = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MuflonIII = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ceniky", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dodavatele_CenikId",
                table: "Dodavatele",
                column: "CenikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dodavatele_Ceniky_CenikId",
                table: "Dodavatele",
                column: "CenikId",
                principalTable: "Ceniky",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dodavatele_Ceniky_CenikId",
                table: "Dodavatele");

            migrationBuilder.DropTable(
                name: "Ceniky");

            migrationBuilder.DropIndex(
                name: "IX_Dodavatele_CenikId",
                table: "Dodavatele");

            migrationBuilder.DropColumn(
                name: "CenikId",
                table: "Dodavatele");

            migrationBuilder.AlterColumn<string>(
                name: "Dodavatel",
                table: "Dodavatele",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
