using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuRa_Vykup.Data.Migrations
{
    /// <inheritdoc />
    public partial class opravaCizychKlicu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vykup_Ceniky_CenikyId",
                table: "Vykup");

            migrationBuilder.DropForeignKey(
                name: "FK_Vykup_PokladniDoklady_IdPokladniDoklad",
                table: "Vykup");

            migrationBuilder.DropForeignKey(
                name: "FK_Vykup_Traces_IdTraces",
                table: "Vykup");

            migrationBuilder.AddForeignKey(
                name: "FK_Vykup_Ceniky_CenikyId",
                table: "Vykup",
                column: "CenikyId",
                principalTable: "Ceniky",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Vykup_PokladniDoklady_IdPokladniDoklad",
                table: "Vykup",
                column: "IdPokladniDoklad",
                principalTable: "PokladniDoklady",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Vykup_Traces_IdTraces",
                table: "Vykup",
                column: "IdTraces",
                principalTable: "Traces",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vykup_Ceniky_CenikyId",
                table: "Vykup");

            migrationBuilder.DropForeignKey(
                name: "FK_Vykup_PokladniDoklady_IdPokladniDoklad",
                table: "Vykup");

            migrationBuilder.DropForeignKey(
                name: "FK_Vykup_Traces_IdTraces",
                table: "Vykup");

            migrationBuilder.AddForeignKey(
                name: "FK_Vykup_Ceniky_CenikyId",
                table: "Vykup",
                column: "CenikyId",
                principalTable: "Ceniky",
                principalColumn: "Id");

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
    }
}
