using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProgsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProgEpisodeId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProgEpisodeId",
                table: "Attendances",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Progs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PauseState = table.Column<int>(type: "int", nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Progs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Progs_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LogProgs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                name: "ProgEpisode",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    AttendType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LimitStudents = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProgEpStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProgEpEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Period = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProgId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EpisodeTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PauseState = table.Column<int>(type: "int", nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: true),
                    ProgId2 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgEpisode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgEpisode_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProgEpisode_EpisodesTypes_EpisodeTypeId",
                        column: x => x.EpisodeTypeId,
                        principalTable: "EpisodesTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProgEpisode_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProgEpisode_Locations_ProgId",
                        column: x => x.ProgId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProgEpisode_Progs_ProgId2",
                        column: x => x.ProgId2,
                        principalTable: "Progs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_ProgEpisodeId",
                table: "Students",
                column: "ProgEpisodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_ProgEpisodeId",
                table: "Attendances",
                column: "ProgEpisodeId");

            migrationBuilder.CreateIndex(
                name: "IX_LogProgs_ProgId",
                table: "LogProgs",
                column: "ProgId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgEpisode_ApplicationUserId",
                table: "ProgEpisode",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgEpisode_EpisodeTypeId",
                table: "ProgEpisode",
                column: "EpisodeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgEpisode_LocationId",
                table: "ProgEpisode",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgEpisode_ProgId",
                table: "ProgEpisode",
                column: "ProgId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgEpisode_ProgId2",
                table: "ProgEpisode",
                column: "ProgId2");

            migrationBuilder.CreateIndex(
                name: "IX_Progs_ApplicationUserId",
                table: "Progs",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_ProgEpisode_ProgEpisodeId",
                table: "Attendances",
                column: "ProgEpisodeId",
                principalTable: "ProgEpisode",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_ProgEpisode_ProgEpisodeId",
                table: "Students",
                column: "ProgEpisodeId",
                principalTable: "ProgEpisode",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_ProgEpisode_ProgEpisodeId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_ProgEpisode_ProgEpisodeId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "LogProgs");

            migrationBuilder.DropTable(
                name: "ProgEpisode");

            migrationBuilder.DropTable(
                name: "Progs");

            migrationBuilder.DropIndex(
                name: "IX_Students_ProgEpisodeId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_ProgEpisodeId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "ProgEpisodeId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ProgEpisodeId",
                table: "Attendances");
        }
    }
}
