using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class StudentProgEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentEpisodeProgs");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Progs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Episodes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "EpisodeProgs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.CreateTable(
                name: "StudentProgs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ParentPhoneNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentProgs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogStudentProgs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StudentProgId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogStudentProgs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogStudentProgs_StudentProgs_StudentProgId",
                        column: x => x.StudentProgId,
                        principalTable: "StudentProgs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentProgEpisodeProgs",
                columns: table => new
                {
                    StudentProgId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EpisodeProgId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentProgEpisodeProgs", x => new { x.EpisodeProgId, x.StudentProgId });
                    table.ForeignKey(
                        name: "FK_StudentProgEpisodeProgs_EpisodeProgs_EpisodeProgId",
                        column: x => x.EpisodeProgId,
                        principalTable: "EpisodeProgs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentProgEpisodeProgs_StudentProgs_StudentProgId",
                        column: x => x.StudentProgId,
                        principalTable: "StudentProgs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogStudentProgs_StudentProgId",
                table: "LogStudentProgs",
                column: "StudentProgId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentProgEpisodeProgs_StudentProgId",
                table: "StudentProgEpisodeProgs",
                column: "StudentProgId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogStudentProgs");

            migrationBuilder.DropTable(
                name: "StudentProgEpisodeProgs");

            migrationBuilder.DropTable(
                name: "StudentProgs");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Progs",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Episodes",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "EpisodeProgs",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.CreateTable(
                name: "StudentEpisodeProgs",
                columns: table => new
                {
                    EpisodeProgId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentEpisodeProgs", x => new { x.EpisodeProgId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_StudentEpisodeProgs_EpisodeProgs_EpisodeProgId",
                        column: x => x.EpisodeProgId,
                        principalTable: "EpisodeProgs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentEpisodeProgs_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentEpisodeProgs_StudentId",
                table: "StudentEpisodeProgs",
                column: "StudentId");
        }
    }
}
