using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuRa_Vykup.Data.Migrations
{
    /// <inheritdoc />
    public partial class dodacilist_vazba : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdDodavatel",
                table: "DodaciListy",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DodaciListy_IdDodavatel",
                table: "DodaciListy",
                column: "IdDodavatel");

            migrationBuilder.AddForeignKey(
                name: "FK_DodaciListy_Dodavatele_IdDodavatel",
                table: "DodaciListy",
                column: "IdDodavatel",
                principalTable: "Dodavatele",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DodaciListy_Dodavatele_IdDodavatel",
                table: "DodaciListy");

            migrationBuilder.DropIndex(
                name: "IX_DodaciListy_IdDodavatel",
                table: "DodaciListy");

            migrationBuilder.DropColumn(
                name: "IdDodavatel",
                table: "DodaciListy");
        }
    }
}
