using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class DBV12OtherPauseAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PauseState",
                table: "Locations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PauseState",
                table: "Centers",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PauseState",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "PauseState",
                table: "Centers");
        }
    }
}
