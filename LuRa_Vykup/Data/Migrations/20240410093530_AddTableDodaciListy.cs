using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuRa_Vykup.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTableDodaciListy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CisloDL",
                table: "Vykup",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DodaciListy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumVystaveni = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CisloDL = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdUzivatele = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DodaciListy", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vykup_CisloDL",
                table: "Vykup",
                column: "CisloDL");

            migrationBuilder.AddForeignKey(
                name: "FK_Vykup_DodaciListy_CisloDL",
                table: "Vykup",
                column: "CisloDL",
                principalTable: "DodaciListy",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vykup_DodaciListy_CisloDL",
                table: "Vykup");

            migrationBuilder.DropTable(
                name: "DodaciListy");

            migrationBuilder.DropIndex(
                name: "IX_Vykup_CisloDL",
                table: "Vykup");

            migrationBuilder.DropColumn(
                name: "CisloDL",
                table: "Vykup");
        }
    }
}
