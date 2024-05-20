using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuRa_Vykup.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVykupnaAndVykup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vykupna_Dodavatele_IdDodavatel",
                table: "Vykupna");

            migrationBuilder.DropIndex(
                name: "IX_Vykupna_IdDodavatel",
                table: "Vykupna");

            migrationBuilder.DropColumn(
                name: "IdDodavatel",
                table: "Vykupna");

            migrationBuilder.AddColumn<int>(
                name: "VykupnaId",
                table: "Vykup",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vykup_VykupnaId",
                table: "Vykup",
                column: "VykupnaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vykup_Vykupna_VykupnaId",
                table: "Vykup",
                column: "VykupnaId",
                principalTable: "Vykupna",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vykup_Vykupna_VykupnaId",
                table: "Vykup");

            migrationBuilder.DropIndex(
                name: "IX_Vykup_VykupnaId",
                table: "Vykup");

            migrationBuilder.DropColumn(
                name: "VykupnaId",
                table: "Vykup");

            migrationBuilder.AddColumn<int>(
                name: "IdDodavatel",
                table: "Vykupna",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vykupna_IdDodavatel",
                table: "Vykupna",
                column: "IdDodavatel");

            migrationBuilder.AddForeignKey(
                name: "FK_Vykupna_Dodavatele_IdDodavatel",
                table: "Vykupna",
                column: "IdDodavatel",
                principalTable: "Dodavatele",
                principalColumn: "Id");
        }
    }
}
