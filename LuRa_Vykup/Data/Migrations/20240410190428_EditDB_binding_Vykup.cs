using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuRa_Vykup.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditDB_binding_Vykup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vykup_DodaciListy_CisloDL",
                table: "Vykup");

            migrationBuilder.RenameColumn(
                name: "CisloDL",
                table: "Vykup",
                newName: "DodaciListId");

            migrationBuilder.RenameIndex(
                name: "IX_Vykup_CisloDL",
                table: "Vykup",
                newName: "IX_Vykup_DodaciListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vykup_DodaciListy_DodaciListId",
                table: "Vykup",
                column: "DodaciListId",
                principalTable: "DodaciListy",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vykup_DodaciListy_DodaciListId",
                table: "Vykup");

            migrationBuilder.RenameColumn(
                name: "DodaciListId",
                table: "Vykup",
                newName: "CisloDL");

            migrationBuilder.RenameIndex(
                name: "IX_Vykup_DodaciListId",
                table: "Vykup",
                newName: "IX_Vykup_CisloDL");

            migrationBuilder.AddForeignKey(
                name: "FK_Vykup_DodaciListy_CisloDL",
                table: "Vykup",
                column: "CisloDL",
                principalTable: "DodaciListy",
                principalColumn: "Id");
        }
    }
}
