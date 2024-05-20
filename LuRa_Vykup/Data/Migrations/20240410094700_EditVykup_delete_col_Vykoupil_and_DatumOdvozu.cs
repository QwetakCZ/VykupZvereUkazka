using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuRa_Vykup.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditVykup_delete_col_Vykoupil_and_DatumOdvozu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DatumOdvozu",
                table: "Vykup");

            migrationBuilder.DropColumn(
                name: "Vykoupil",
                table: "Vykup");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DatumOdvozu",
                table: "Vykup",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Vykoupil",
                table: "Vykup",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");
        }
    }
}
