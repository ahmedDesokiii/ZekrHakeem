using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class LessonEntityModify : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_CurrentLessons_StCurrentLessonId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_NextLessons_StNextLessonId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_PrevLessons_StPrevLessonId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "LogCurrentLessons");

            migrationBuilder.DropTable(
                name: "LogNextLessons");

            migrationBuilder.DropTable(
                name: "LogPrevLessons");

            migrationBuilder.DropTable(
                name: "CurrentLessons");

            migrationBuilder.DropTable(
                name: "NextLessons");

            migrationBuilder.DropTable(
                name: "PrevLessons");

            migrationBuilder.DropIndex(
                name: "IX_Students_StCurrentLessonId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_StNextLessonId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_StPrevLessonId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StCurrentLessonId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StNextLessonId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StPrevLessonId",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "Save_ToSorh",
                table: "Lessons",
                newName: "Prev_Save_ToSorh");

            migrationBuilder.RenameColumn(
                name: "Save_ToAyah",
                table: "Lessons",
                newName: "Prev_Save_ToAyah");

            migrationBuilder.RenameColumn(
                name: "Save_Satr",
                table: "Lessons",
                newName: "Prev_Save_Satr");

            migrationBuilder.RenameColumn(
                name: "Save_FromSorh",
                table: "Lessons",
                newName: "Prev_Save_FromSorh");

            migrationBuilder.RenameColumn(
                name: "Save_FromAyah",
                table: "Lessons",
                newName: "Prev_Save_FromAyah");

            migrationBuilder.RenameColumn(
                name: "Rev_ToSorh",
                table: "Lessons",
                newName: "Prev_Rev_ToSorh");

            migrationBuilder.RenameColumn(
                name: "Rev_ToAyah",
                table: "Lessons",
                newName: "Prev_Rev_ToAyah");

            migrationBuilder.RenameColumn(
                name: "Rev_Satr",
                table: "Lessons",
                newName: "Prev_Rev_Satr");

            migrationBuilder.RenameColumn(
                name: "Rev_FromSorh",
                table: "Lessons",
                newName: "Prev_Rev_FromSorh");

            migrationBuilder.RenameColumn(
                name: "Rev_FromAyah",
                table: "Lessons",
                newName: "Prev_Rev_FromAyah");

            migrationBuilder.RenameColumn(
                name: "Rabt_ToSorh",
                table: "Lessons",
                newName: "Prev_Rabt_ToSorh");

            migrationBuilder.RenameColumn(
                name: "Rabt_ToAyah",
                table: "Lessons",
                newName: "Prev_Rabt_ToAyah");

            migrationBuilder.RenameColumn(
                name: "Rabt_Satr",
                table: "Lessons",
                newName: "Prev_Rabt_Satr");

            migrationBuilder.RenameColumn(
                name: "Rabt_FromSorh",
                table: "Lessons",
                newName: "Prev_Rabt_FromSorh");

            migrationBuilder.RenameColumn(
                name: "Rabt_FromAyah",
                table: "Lessons",
                newName: "Prev_Rabt_FromAyah");

            migrationBuilder.AddColumn<int>(
                name: "Curr_Rabt_FromAyah",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Curr_Rabt_FromSorh",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Curr_Rabt_Satr",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Curr_Rabt_ToAyah",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Curr_Rabt_ToSorh",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Curr_Rev_FromAyah",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Curr_Rev_FromSorh",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Curr_Rev_Satr",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Curr_Rev_ToAyah",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Curr_Rev_ToSorh",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Curr_Save_FromAyah",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Curr_Save_FromSorh",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Curr_Save_Satr",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Curr_Save_ToAyah",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Curr_Save_ToSorh",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Next_Rabt_FromAyah",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Next_Rabt_FromSorh",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Next_Rabt_Satr",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Next_Rabt_ToAyah",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Next_Rabt_ToSorh",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Next_Rev_FromAyah",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Next_Rev_FromSorh",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Next_Rev_Satr",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Next_Rev_ToAyah",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Next_Rev_ToSorh",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Next_Save_FromAyah",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Next_Save_FromSorh",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Next_Save_Satr",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Next_Save_ToAyah",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Next_Save_ToSorh",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Curr_Rabt_FromAyah",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Curr_Rabt_FromSorh",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Curr_Rabt_Satr",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Curr_Rabt_ToAyah",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Curr_Rabt_ToSorh",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Curr_Rev_FromAyah",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Curr_Rev_FromSorh",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Curr_Rev_Satr",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Curr_Rev_ToAyah",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Curr_Rev_ToSorh",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Curr_Save_FromAyah",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Curr_Save_FromSorh",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Curr_Save_Satr",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Curr_Save_ToAyah",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Curr_Save_ToSorh",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Next_Rabt_FromAyah",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Next_Rabt_FromSorh",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Next_Rabt_Satr",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Next_Rabt_ToAyah",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Next_Rabt_ToSorh",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Next_Rev_FromAyah",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Next_Rev_FromSorh",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Next_Rev_Satr",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Next_Rev_ToAyah",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Next_Rev_ToSorh",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Next_Save_FromAyah",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Next_Save_FromSorh",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Next_Save_Satr",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Next_Save_ToAyah",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Next_Save_ToSorh",
                table: "Lessons");

            migrationBuilder.RenameColumn(
                name: "Prev_Save_ToSorh",
                table: "Lessons",
                newName: "Save_ToSorh");

            migrationBuilder.RenameColumn(
                name: "Prev_Save_ToAyah",
                table: "Lessons",
                newName: "Save_ToAyah");

            migrationBuilder.RenameColumn(
                name: "Prev_Save_Satr",
                table: "Lessons",
                newName: "Save_Satr");

            migrationBuilder.RenameColumn(
                name: "Prev_Save_FromSorh",
                table: "Lessons",
                newName: "Save_FromSorh");

            migrationBuilder.RenameColumn(
                name: "Prev_Save_FromAyah",
                table: "Lessons",
                newName: "Save_FromAyah");

            migrationBuilder.RenameColumn(
                name: "Prev_Rev_ToSorh",
                table: "Lessons",
                newName: "Rev_ToSorh");

            migrationBuilder.RenameColumn(
                name: "Prev_Rev_ToAyah",
                table: "Lessons",
                newName: "Rev_ToAyah");

            migrationBuilder.RenameColumn(
                name: "Prev_Rev_Satr",
                table: "Lessons",
                newName: "Rev_Satr");

            migrationBuilder.RenameColumn(
                name: "Prev_Rev_FromSorh",
                table: "Lessons",
                newName: "Rev_FromSorh");

            migrationBuilder.RenameColumn(
                name: "Prev_Rev_FromAyah",
                table: "Lessons",
                newName: "Rev_FromAyah");

            migrationBuilder.RenameColumn(
                name: "Prev_Rabt_ToSorh",
                table: "Lessons",
                newName: "Rabt_ToSorh");

            migrationBuilder.RenameColumn(
                name: "Prev_Rabt_ToAyah",
                table: "Lessons",
                newName: "Rabt_ToAyah");

            migrationBuilder.RenameColumn(
                name: "Prev_Rabt_Satr",
                table: "Lessons",
                newName: "Rabt_Satr");

            migrationBuilder.RenameColumn(
                name: "Prev_Rabt_FromSorh",
                table: "Lessons",
                newName: "Rabt_FromSorh");

            migrationBuilder.RenameColumn(
                name: "Prev_Rabt_FromAyah",
                table: "Lessons",
                newName: "Rabt_FromAyah");

            migrationBuilder.AddColumn<Guid>(
                name: "StCurrentLessonId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "StNextLessonId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "StPrevLessonId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "CurrentLessons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    CurrentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    Num = table.Column<int>(type: "int", nullable: false),
                    Rabt_FromAyah = table.Column<int>(type: "int", nullable: false),
                    Rabt_FromSorh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rabt_Satr = table.Column<int>(type: "int", nullable: false),
                    Rabt_ToAyah = table.Column<int>(type: "int", nullable: false),
                    Rabt_ToSorh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rev_FromAyah = table.Column<int>(type: "int", nullable: false),
                    Rev_FromSorh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rev_Satr = table.Column<int>(type: "int", nullable: false),
                    Rev_ToAyah = table.Column<int>(type: "int", nullable: false),
                    Rev_ToSorh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Save_FromAyah = table.Column<int>(type: "int", nullable: false),
                    Save_FromSorh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Save_Satr = table.Column<int>(type: "int", nullable: false),
                    Save_ToAyah = table.Column<int>(type: "int", nullable: false),
                    Save_ToSorh = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentLessons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NextLessons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    NextDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Num = table.Column<int>(type: "int", nullable: false),
                    Rabt_FromAyah = table.Column<int>(type: "int", nullable: false),
                    Rabt_FromSorh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rabt_Satr = table.Column<int>(type: "int", nullable: false),
                    Rabt_ToAyah = table.Column<int>(type: "int", nullable: false),
                    Rabt_ToSorh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rev_FromAyah = table.Column<int>(type: "int", nullable: false),
                    Rev_FromSorh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rev_Satr = table.Column<int>(type: "int", nullable: false),
                    Rev_ToAyah = table.Column<int>(type: "int", nullable: false),
                    Rev_ToSorh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Save_FromAyah = table.Column<int>(type: "int", nullable: false),
                    Save_FromSorh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Save_Satr = table.Column<int>(type: "int", nullable: false),
                    Save_ToAyah = table.Column<int>(type: "int", nullable: false),
                    Save_ToSorh = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NextLessons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrevLessons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    Num = table.Column<int>(type: "int", nullable: false),
                    PrevDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rabt_FromAyah = table.Column<int>(type: "int", nullable: false),
                    Rabt_FromSorh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rabt_Satr = table.Column<int>(type: "int", nullable: false),
                    Rabt_ToAyah = table.Column<int>(type: "int", nullable: false),
                    Rabt_ToSorh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rev_FromAyah = table.Column<int>(type: "int", nullable: false),
                    Rev_FromSorh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rev_Satr = table.Column<int>(type: "int", nullable: false),
                    Rev_ToAyah = table.Column<int>(type: "int", nullable: false),
                    Rev_ToSorh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Save_FromAyah = table.Column<int>(type: "int", nullable: false),
                    Save_FromSorh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Save_Satr = table.Column<int>(type: "int", nullable: false),
                    Save_ToAyah = table.Column<int>(type: "int", nullable: false),
                    Save_ToSorh = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrevLessons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogCurrentLessons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    CurrentLessonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogCurrentLessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogCurrentLessons_CurrentLessons_CurrentLessonId",
                        column: x => x.CurrentLessonId,
                        principalTable: "CurrentLessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LogNextLessons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    NextLessonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "LogPrevLessons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    PrevLessonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogPrevLessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogPrevLessons_PrevLessons_PrevLessonId",
                        column: x => x.PrevLessonId,
                        principalTable: "PrevLessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_StCurrentLessonId",
                table: "Students",
                column: "StCurrentLessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_StNextLessonId",
                table: "Students",
                column: "StNextLessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_StPrevLessonId",
                table: "Students",
                column: "StPrevLessonId");

            migrationBuilder.CreateIndex(
                name: "IX_LogCurrentLessons_CurrentLessonId",
                table: "LogCurrentLessons",
                column: "CurrentLessonId");

            migrationBuilder.CreateIndex(
                name: "IX_LogNextLessons_NextLessonId",
                table: "LogNextLessons",
                column: "NextLessonId");

            migrationBuilder.CreateIndex(
                name: "IX_LogPrevLessons_PrevLessonId",
                table: "LogPrevLessons",
                column: "PrevLessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_CurrentLessons_StCurrentLessonId",
                table: "Students",
                column: "StCurrentLessonId",
                principalTable: "CurrentLessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_NextLessons_StNextLessonId",
                table: "Students",
                column: "StNextLessonId",
                principalTable: "NextLessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_PrevLessons_StPrevLessonId",
                table: "Students",
                column: "StPrevLessonId",
                principalTable: "PrevLessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
