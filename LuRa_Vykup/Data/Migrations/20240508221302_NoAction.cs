using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuRa_Vykup.Data.Migrations
{
    /// <inheritdoc />
    public partial class NoAction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DodaciListy_Dodavatele_IdDodavatel",
                table: "DodaciListy");

            migrationBuilder.DropForeignKey(
                name: "FK_Dodavatele_Ceniky_CenikId",
                table: "Dodavatele");

            migrationBuilder.DropForeignKey(
                name: "FK_PokladniDoklady_Ceniky_IdCenik",
                table: "PokladniDoklady");

            migrationBuilder.DropForeignKey(
                name: "FK_PokladniDoklady_Dodavatele_IdDodavatel",
                table: "PokladniDoklady");

            migrationBuilder.DropForeignKey(
                name: "FK_Vykup_Dodavatele_IdDodavatel",
                table: "Vykup");

            migrationBuilder.DropForeignKey(
                name: "FK_Vykup_Vykupna_VykupnaId",
                table: "Vykup");

            migrationBuilder.AddForeignKey(
                name: "FK_DodaciListy_Dodavatele_IdDodavatel",
                table: "DodaciListy",
                column: "IdDodavatel",
                principalTable: "Dodavatele",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dodavatele_Ceniky_CenikId",
                table: "Dodavatele",
                column: "CenikId",
                principalTable: "Ceniky",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PokladniDoklady_Ceniky_IdCenik",
                table: "PokladniDoklady",
                column: "IdCenik",
                principalTable: "Ceniky",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PokladniDoklady_Dodavatele_IdDodavatel",
                table: "PokladniDoklady",
                column: "IdDodavatel",
                principalTable: "Dodavatele",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vykup_Dodavatele_IdDodavatel",
                table: "Vykup",
                column: "IdDodavatel",
                principalTable: "Dodavatele",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vykup_Vykupna_VykupnaId",
                table: "Vykup",
                column: "VykupnaId",
                principalTable: "Vykupna",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DodaciListy_Dodavatele_IdDodavatel",
                table: "DodaciListy");

            migrationBuilder.DropForeignKey(
                name: "FK_Dodavatele_Ceniky_CenikId",
                table: "Dodavatele");

            migrationBuilder.DropForeignKey(
                name: "FK_PokladniDoklady_Ceniky_IdCenik",
                table: "PokladniDoklady");

            migrationBuilder.DropForeignKey(
                name: "FK_PokladniDoklady_Dodavatele_IdDodavatel",
                table: "PokladniDoklady");

            migrationBuilder.DropForeignKey(
                name: "FK_Vykup_Dodavatele_IdDodavatel",
                table: "Vykup");

            migrationBuilder.DropForeignKey(
                name: "FK_Vykup_Vykupna_VykupnaId",
                table: "Vykup");

            migrationBuilder.AddForeignKey(
                name: "FK_DodaciListy_Dodavatele_IdDodavatel",
                table: "DodaciListy",
                column: "IdDodavatel",
                principalTable: "Dodavatele",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dodavatele_Ceniky_CenikId",
                table: "Dodavatele",
                column: "CenikId",
                principalTable: "Ceniky",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokladniDoklady_Ceniky_IdCenik",
                table: "PokladniDoklady",
                column: "IdCenik",
                principalTable: "Ceniky",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokladniDoklady_Dodavatele_IdDodavatel",
                table: "PokladniDoklady",
                column: "IdDodavatel",
                principalTable: "Dodavatele",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vykup_Dodavatele_IdDodavatel",
                table: "Vykup",
                column: "IdDodavatel",
                principalTable: "Dodavatele",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vykup_Vykupna_VykupnaId",
                table: "Vykup",
                column: "VykupnaId",
                principalTable: "Vykupna",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
