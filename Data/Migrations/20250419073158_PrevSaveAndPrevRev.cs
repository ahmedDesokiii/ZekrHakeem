using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class PrevSaveAndPrevRev : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrevRev_FromAyah",
                table: "LessonsData",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrevRev_FromSorh",
                table: "LessonsData",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PrevRev_ToAyah",
                table: "LessonsData",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrevRev_ToSorh",
                table: "LessonsData",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PrevSave_FromAyah",
                table: "LessonsData",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrevSave_FromSorh",
                table: "LessonsData",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PrevSave_ToAyah",
                table: "LessonsData",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrevSave_ToSorh",
                table: "LessonsData",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrevRev_FromAyah",
                table: "LessonsData");

            migrationBuilder.DropColumn(
                name: "PrevRev_FromSorh",
                table: "LessonsData");

            migrationBuilder.DropColumn(
                name: "PrevRev_ToAyah",
                table: "LessonsData");

            migrationBuilder.DropColumn(
                name: "PrevRev_ToSorh",
                table: "LessonsData");

            migrationBuilder.DropColumn(
                name: "PrevSave_FromAyah",
                table: "LessonsData");

            migrationBuilder.DropColumn(
                name: "PrevSave_FromSorh",
                table: "LessonsData");

            migrationBuilder.DropColumn(
                name: "PrevSave_ToAyah",
                table: "LessonsData");

            migrationBuilder.DropColumn(
                name: "PrevSave_ToSorh",
                table: "LessonsData");
        }
    }
}
