using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class AgainUpdateAfterSavedVersion13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StNextLessonId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "NextLessons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Num = table.Column<int>(type: "int", nullable: false),
                    NextDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
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
                    Rev_Satr = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NextLessons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogNextLessons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NextLessonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogNextLessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogNextLessons_NextLessons_NextLessonId",
                        column: x => x.NextLessonId,
                        principalTable: "NextLessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_StNextLessonId",
                table: "Students",
                column: "StNextLessonId");

            migrationBuilder.CreateIndex(
                name: "IX_LogNextLessons_NextLessonId",
                table: "LogNextLessons",
                column: "NextLessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_NextLessons_StNextLessonId",
                table: "Students",
                column: "StNextLessonId",
                principalTable: "NextLessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_NextLessons_StNextLessonId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "LogNextLessons");

            migrationBuilder.DropTable(
                name: "NextLessons");

            migrationBuilder.DropIndex(
                name: "IX_Students_StNextLessonId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StNextLessonId",
                table: "Students");
        }
    }
}
