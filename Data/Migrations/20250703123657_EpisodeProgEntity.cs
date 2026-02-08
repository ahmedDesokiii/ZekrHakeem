using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class EpisodeProgEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EpisodeProgId",
                table: "Attendances",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EpisodeProgs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    AttendType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    ManualFinished = table.Column<bool>(type: "bit", nullable: false),
                    IsAvilable = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CheckDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Period = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EpProgLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EpProgId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StartSurah = table.Column<int>(type: "int", nullable: true),
                    EndSurah = table.Column<int>(type: "int", nullable: true),
                    PauseState = table.Column<int>(type: "int", nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EpisodeProgs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EpisodeProgs_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EpisodeProgs_Locations_EpProgLocationId",
                        column: x => x.EpProgLocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EpisodeProgs_Progs_EpProgId",
                        column: x => x.EpProgId,
                        principalTable: "Progs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LogProgs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogProgs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogProgs_Progs_ProgId",
                        column: x => x.ProgId,
                        principalTable: "Progs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LogEpisodeProgs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EpisodeProgId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogEpisodeProgs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogEpisodeProgs_EpisodeProgs_EpisodeProgId",
                        column: x => x.EpisodeProgId,
                        principalTable: "EpisodeProgs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentEpisodeProgs",
                columns: table => new
                {
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EpisodeProgId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                name: "IX_Attendances_EpisodeProgId",
                table: "Attendances",
                column: "EpisodeProgId");

            migrationBuilder.CreateIndex(
                name: "IX_EpisodeProgs_ApplicationUserId",
                table: "EpisodeProgs",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EpisodeProgs_EpProgId",
                table: "EpisodeProgs",
                column: "EpProgId");

            migrationBuilder.CreateIndex(
                name: "IX_EpisodeProgs_EpProgLocationId",
                table: "EpisodeProgs",
                column: "EpProgLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_LogEpisodeProgs_EpisodeProgId",
                table: "LogEpisodeProgs",
                column: "EpisodeProgId");

            migrationBuilder.CreateIndex(
                name: "IX_LogProgs_ProgId",
                table: "LogProgs",
                column: "ProgId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentEpisodeProgs_StudentId",
                table: "StudentEpisodeProgs",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_EpisodeProgs_EpisodeProgId",
                table: "Attendances",
                column: "EpisodeProgId",
                principalTable: "EpisodeProgs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_EpisodeProgs_EpisodeProgId",
                table: "Attendances");

            migrationBuilder.DropTable(
                name: "LogEpisodeProgs");

            migrationBuilder.DropTable(
                name: "LogProgs");

            migrationBuilder.DropTable(
                name: "StudentEpisodeProgs");

            migrationBuilder.DropTable(
                name: "EpisodeProgs");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_EpisodeProgId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "EpisodeProgId",
                table: "Attendances");
        }
    }
}
