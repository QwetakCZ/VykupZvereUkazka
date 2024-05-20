using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuRa_Vykup.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_New_DB_Vykupny : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vykupna",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazev = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Ulice = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CisloPopisne = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Mesto = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Psc = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    IdDodavatel = table.Column<int>(type: "int", nullable: true),
                    IdUzivatel = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vykupna", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vykupna_Dodavatele_IdDodavatel",
                        column: x => x.IdDodavatel,
                        principalTable: "Dodavatele",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vykupna_IdDodavatel",
                table: "Vykupna",
                column: "IdDodavatel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vykupna");
        }
    }
}
