using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class DBV12_AddCurrentStateAttribute_SomeTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentState",
                table: "Nationalities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CurrentState",
                table: "HealthConditions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CurrentState",
                table: "EpisodesTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentState",
                table: "Nationalities");

            migrationBuilder.DropColumn(
                name: "CurrentState",
                table: "HealthConditions");

            migrationBuilder.DropColumn(
                name: "CurrentState",
                table: "EpisodesTypes");
        }
    }
}
