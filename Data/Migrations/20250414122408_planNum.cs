using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class planNum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Num",
                table: "PlansData",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Surahs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentState = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surahs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Verses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    VerseText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Letters = table.Column<int>(type: "int", nullable: true),
                    Lines = table.Column<float>(type: "real", nullable: true),
                    Section = table.Column<int>(type: "int", nullable: true),
                    SurahId = table.Column<int>(type: "int", nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Verses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Verses_Surahs_SurahId",
                        column: x => x.SurahId,
                        principalTable: "Surahs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Verses_SurahId",
                table: "Verses",
                column: "SurahId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Verses");

            migrationBuilder.DropTable(
                name: "Surahs");

            migrationBuilder.DropColumn(
                name: "Num",
                table: "PlansData");
        }
    }
}
