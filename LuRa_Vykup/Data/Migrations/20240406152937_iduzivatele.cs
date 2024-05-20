using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuRa_Vykup.Data.Migrations
{
    /// <inheritdoc />
    public partial class iduzivatele : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vykup_IdDodavatel",
                table: "Vykup");

            migrationBuilder.AddColumn<string>(
                name: "IdUzivatele",
                table: "Vykup",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IdUzivatel",
                table: "Dodavatele",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vykup_IdDodavatel",
                table: "Vykup",
                column: "IdDodavatel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vykup_IdDodavatel",
                table: "Vykup");

            migrationBuilder.DropColumn(
                name: "IdUzivatele",
                table: "Vykup");

            migrationBuilder.DropColumn(
                name: "IdUzivatel",
                table: "Dodavatele");

            migrationBuilder.CreateIndex(
                name: "IX_Vykup_IdDodavatel",
                table: "Vykup",
                column: "IdDodavatel",
                unique: true);
        }
    }
}
