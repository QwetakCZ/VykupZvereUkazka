﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuRa_Vykup.Data.Migrations
{
    /// <inheritdoc />
    public partial class vazby2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vykup_IdDodavatel",
                table: "Vykup");

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

            migrationBuilder.CreateIndex(
                name: "IX_Vykup_IdDodavatel",
                table: "Vykup",
                column: "IdDodavatel",
                unique: true);
        }
    }
}
