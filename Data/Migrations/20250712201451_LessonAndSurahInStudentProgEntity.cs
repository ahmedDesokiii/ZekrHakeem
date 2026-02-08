using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class LessonAndSurahInStudentProgEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEvaluate",
                table: "StudentProgs",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LessonDataId",
                table: "StudentProgs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrevSaveSurhTo",
                table: "StudentProgs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PrevSaveVerseTo",
                table: "StudentProgs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SurahId",
                table: "StudentProgs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "newLessonState",
                table: "StudentProgs",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentProgs_LessonDataId",
                table: "StudentProgs",
                column: "LessonDataId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentProgs_SurahId",
                table: "StudentProgs",
                column: "SurahId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentProgs_LessonsData_LessonDataId",
                table: "StudentProgs",
                column: "LessonDataId",
                principalTable: "LessonsData",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentProgs_Surahs_SurahId",
                table: "StudentProgs",
                column: "SurahId",
                principalTable: "Surahs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentProgs_LessonsData_LessonDataId",
                table: "StudentProgs");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentProgs_Surahs_SurahId",
                table: "StudentProgs");

            migrationBuilder.DropIndex(
                name: "IX_StudentProgs_LessonDataId",
                table: "StudentProgs");

            migrationBuilder.DropIndex(
                name: "IX_StudentProgs_SurahId",
                table: "StudentProgs");

            migrationBuilder.DropColumn(
                name: "IsEvaluate",
                table: "StudentProgs");

            migrationBuilder.DropColumn(
                name: "LessonDataId",
                table: "StudentProgs");

            migrationBuilder.DropColumn(
                name: "PrevSaveSurhTo",
                table: "StudentProgs");

            migrationBuilder.DropColumn(
                name: "PrevSaveVerseTo",
                table: "StudentProgs");

            migrationBuilder.DropColumn(
                name: "SurahId",
                table: "StudentProgs");

            migrationBuilder.DropColumn(
                name: "newLessonState",
                table: "StudentProgs");
        }
    }
}
