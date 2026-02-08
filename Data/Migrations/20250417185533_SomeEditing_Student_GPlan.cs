using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class SomeEditing_Student_GPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Num",
                table: "Verses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SaveDirection",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VerseId",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_VerseId",
                table: "Students",
                column: "VerseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Verses_VerseId",
                table: "Students",
                column: "VerseId",
                principalTable: "Verses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Verses_VerseId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_VerseId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Num",
                table: "Verses");

            migrationBuilder.DropColumn(
                name: "SaveDirection",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "VerseId",
                table: "Students");
        }
    }
}
