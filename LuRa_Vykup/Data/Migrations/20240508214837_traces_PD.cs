using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuRa_Vykup.Data.Migrations
{
    /// <inheritdoc />
    public partial class traces_PD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CenaKgPokladniDoklad",
                table: "Vykup",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdPokladniDoklad",
                table: "Vykup",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdTraces",
                table: "Vykup",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PokladniDoklady",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CisloDokladu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUzivatele = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdDodavatelPokladna = table.Column<int>(type: "int", nullable: false),
                    IdCenik = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokladniDoklady", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokladniDoklady_Ceniky_IdCenik",
                        column: x => x.IdCenik,
                        principalTable: "Ceniky",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PokladniDoklady_Dodavatele_IdDodavatelPokladna",
                        column: x => x.IdDodavatelPokladna,
                        principalTable: "Dodavatele",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Traces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CisloTraces = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdUzivatele = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Traces", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vykup_IdPokladniDoklad",
                table: "Vykup",
                column: "IdPokladniDoklad");

            migrationBuilder.CreateIndex(
                name: "IX_Vykup_IdTraces",
                table: "Vykup",
                column: "IdTraces");

            migrationBuilder.CreateIndex(
                name: "IX_PokladniDoklady_IdCenik",
                table: "PokladniDoklady",
                column: "IdCenik");

            migrationBuilder.CreateIndex(
                name: "IX_PokladniDoklady_IdDodavatelPokladna",
                table: "PokladniDoklady",
                column: "IdDodavatelPokladna");

            migrationBuilder.AddForeignKey(
                name: "FK_Vykup_PokladniDoklady_IdPokladniDoklad",
                table: "Vykup",
                column: "IdPokladniDoklad",
                principalTable: "PokladniDoklady",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vykup_Traces_IdTraces",
                table: "Vykup",
                column: "IdTraces",
                principalTable: "Traces",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vykup_PokladniDoklady_IdPokladniDoklad",
                table: "Vykup");

            migrationBuilder.DropForeignKey(
                name: "FK_Vykup_Traces_IdTraces",
                table: "Vykup");

            migrationBuilder.DropTable(
                name: "PokladniDoklady");

            migrationBuilder.DropTable(
                name: "Traces");

            migrationBuilder.DropIndex(
                name: "IX_Vykup_IdPokladniDoklad",
                table: "Vykup");

            migrationBuilder.DropIndex(
                name: "IX_Vykup_IdTraces",
                table: "Vykup");

            migrationBuilder.DropColumn(
                name: "CenaKgPokladniDoklad",
                table: "Vykup");

            migrationBuilder.DropColumn(
                name: "IdPokladniDoklad",
                table: "Vykup");

            migrationBuilder.DropColumn(
                name: "IdTraces",
                table: "Vykup");
        }
    }
}
