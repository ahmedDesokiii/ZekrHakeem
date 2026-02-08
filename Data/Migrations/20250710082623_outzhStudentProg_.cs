using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class outzhStudentProg_ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "outZH",
                table: "StudentProgs",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "outZH",
                table: "StudentProgs");
        }
    }
}
