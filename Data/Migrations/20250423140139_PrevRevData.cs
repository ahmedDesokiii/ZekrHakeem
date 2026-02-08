using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class PrevRevData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrevRevVerseFrom",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PrevRevVerseTo",
                table: "Students",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrevRevVerseFrom",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PrevRevVerseTo",
                table: "Students");
        }
    }
}
