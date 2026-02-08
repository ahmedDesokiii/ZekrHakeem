using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class VersesSection_Pages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Section",
                table: "Verses");

            migrationBuilder.AddColumn<float>(
                name: "Pages_ctp",
                table: "Verses",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Pages_ptc",
                table: "Verses",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Sections_ctp",
                table: "Verses",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Sections_ptc",
                table: "Verses",
                type: "real",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pages_ctp",
                table: "Verses");

            migrationBuilder.DropColumn(
                name: "Pages_ptc",
                table: "Verses");

            migrationBuilder.DropColumn(
                name: "Sections_ctp",
                table: "Verses");

            migrationBuilder.DropColumn(
                name: "Sections_ptc",
                table: "Verses");

            migrationBuilder.AddColumn<int>(
                name: "Section",
                table: "Verses",
                type: "int",
                nullable: true);
        }
    }
}
