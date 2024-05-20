using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuRa_Vykup.Data.Migrations
{
    /// <inheritdoc />
    public partial class vykupnaid2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdVykupna",
                table: "Dodavatele",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dodavatele_IdVykupna",
                table: "Dodavatele",
                column: "IdVykupna");

            migrationBuilder.AddForeignKey(
                name: "FK_Dodavatele_Vykupna_IdVykupna",
                table: "Dodavatele",
                column: "IdVykupna",
                principalTable: "Vykupna",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dodavatele_Vykupna_IdVykupna",
                table: "Dodavatele");

            migrationBuilder.DropIndex(
                name: "IX_Dodavatele_IdVykupna",
                table: "Dodavatele");

            migrationBuilder.DropColumn(
                name: "IdVykupna",
                table: "Dodavatele");
        }
    }
}
