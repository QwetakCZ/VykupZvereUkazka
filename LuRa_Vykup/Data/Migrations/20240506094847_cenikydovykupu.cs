using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuRa_Vykup.Data.Migrations
{
    /// <inheritdoc />
    public partial class cenikydovykupu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CenikyId",
                table: "Vykup",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vykup_CenikyId",
                table: "Vykup",
                column: "CenikyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vykup_Ceniky_CenikyId",
                table: "Vykup",
                column: "CenikyId",
                principalTable: "Ceniky",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vykup_Ceniky_CenikyId",
                table: "Vykup");

            migrationBuilder.DropIndex(
                name: "IX_Vykup_CenikyId",
                table: "Vykup");

            migrationBuilder.DropColumn(
                name: "CenikyId",
                table: "Vykup");
        }
    }
}
