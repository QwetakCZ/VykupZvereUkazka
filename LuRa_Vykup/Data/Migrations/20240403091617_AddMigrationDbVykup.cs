using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuRa_Vykup.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMigrationDbVykup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vykup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDodavatel = table.Column<int>(type: "int", nullable: false),
                    Plomba = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Druh = table.Column<int>(type: "int", nullable: false),
                    Vaha = table.Column<float>(type: "real", nullable: false),
                    DatumVykupu = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumOdvozu = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Kategorie = table.Column<int>(type: "int", nullable: false),
                    Vykoupil = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vykup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vykup_Dodavatele_IdDodavatel",
                        column: x => x.IdDodavatel,
                        principalTable: "Dodavatele",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vykup_IdDodavatel",
                table: "Vykup",
                column: "IdDodavatel",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vykup");
        }
    }
}
