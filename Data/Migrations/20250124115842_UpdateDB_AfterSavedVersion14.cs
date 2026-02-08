using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB_AfterSavedVersion14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LessonId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Num = table.Column<int>(type: "int", nullable: false),
                    Save_FromSorh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Save_FromAyah = table.Column<int>(type: "int", nullable: false),
                    Save_ToSorh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Save_ToAyah = table.Column<int>(type: "int", nullable: false),
                    Save_Satr = table.Column<int>(type: "int", nullable: false),
                    Rabt_FromSorh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rabt_FromAyah = table.Column<int>(type: "int", nullable: false),
                    Rabt_ToSorh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rabt_ToAyah = table.Column<int>(type: "int", nullable: false),
                    Rabt_Satr = table.Column<int>(type: "int", nullable: false),
                    Rev_FromSorh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rev_FromAyah = table.Column<int>(type: "int", nullable: false),
                    Rev_ToSorh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rev_ToAyah = table.Column<int>(type: "int", nullable: false),
                    Rev_Satr = table.Column<int>(type: "int", nullable: false),
                    CurrentState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogLessons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LessonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogLessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogLessons_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_LessonId",
                table: "Students",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_LogLessons_LessonId",
                table: "LogLessons",
                column: "LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Lessons_LessonId",
                table: "Students",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Lessons_LessonId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "LogLessons");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Students_LessonId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "LessonId",
                table: "Students");
        }
    }
}
