using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class RelationStudentWithSurah : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SurahId",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_SurahId",
                table: "Students",
                column: "SurahId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Surahs_SurahId",
                table: "Students",
                column: "SurahId",
                principalTable: "Surahs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Surahs_SurahId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_SurahId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "SurahId",
                table: "Students");
        }
    }
}
