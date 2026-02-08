using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class zhlessonsurahId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rabt_FromSurah",
                table: "LessonsData",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rabt_ToSurah",
                table: "LessonsData",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rev_FromSurah",
                table: "LessonsData",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rev_ToSurah",
                table: "LessonsData",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Save_ToSurah",
                table: "LessonsData",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rabt_FromSurah",
                table: "LessonsData");

            migrationBuilder.DropColumn(
                name: "Rabt_ToSurah",
                table: "LessonsData");

            migrationBuilder.DropColumn(
                name: "Rev_FromSurah",
                table: "LessonsData");

            migrationBuilder.DropColumn(
                name: "Rev_ToSurah",
                table: "LessonsData");

            migrationBuilder.DropColumn(
                name: "Save_ToSurah",
                table: "LessonsData");
        }
    }
}
