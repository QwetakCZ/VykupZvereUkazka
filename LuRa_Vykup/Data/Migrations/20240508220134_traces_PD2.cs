using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuRa_Vykup.Data.Migrations
{
    /// <inheritdoc />
    public partial class traces_PD2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokladniDoklady_Dodavatele_IdDodavatelPokladna",
                table: "PokladniDoklady");

            migrationBuilder.RenameColumn(
                name: "IdDodavatelPokladna",
                table: "PokladniDoklady",
                newName: "IdDodavatel");

            migrationBuilder.RenameIndex(
                name: "IX_PokladniDoklady_IdDodavatelPokladna",
                table: "PokladniDoklady",
                newName: "IX_PokladniDoklady_IdDodavatel");

            migrationBuilder.AddForeignKey(
                name: "FK_PokladniDoklady_Dodavatele_IdDodavatel",
                table: "PokladniDoklady",
                column: "IdDodavatel",
                principalTable: "Dodavatele",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokladniDoklady_Dodavatele_IdDodavatel",
                table: "PokladniDoklady");

            migrationBuilder.RenameColumn(
                name: "IdDodavatel",
                table: "PokladniDoklady",
                newName: "IdDodavatelPokladna");

            migrationBuilder.RenameIndex(
                name: "IX_PokladniDoklady_IdDodavatel",
                table: "PokladniDoklady",
                newName: "IX_PokladniDoklady_IdDodavatelPokladna");

            migrationBuilder.AddForeignKey(
                name: "FK_PokladniDoklady_Dodavatele_IdDodavatelPokladna",
                table: "PokladniDoklady",
                column: "IdDodavatelPokladna",
                principalTable: "Dodavatele",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
