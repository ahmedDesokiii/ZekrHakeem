using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class MonsterV42 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "LogProgs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ProgId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProgId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AttendType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAvilable = table.Column<bool>(type: "bit", nullable: false),
                    LimitStudents = table.Column<int>(type: "int", nullable: false),
                    ManualFinished = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PauseState = table.Column<int>(type: "int", nullable: true),
                    Period = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProgEpEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProgEpStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProgId2 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
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
    }
}
