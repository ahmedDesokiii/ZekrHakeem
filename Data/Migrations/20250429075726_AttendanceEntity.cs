using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class AttendanceEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Students",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Statement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Att_In = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Att_Out = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EpisodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendances_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attendances_Episodes_EpisodeId",
                        column: x => x.EpisodeId,
                        principalTable: "Episodes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LogAttendances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttendanceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogAttendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogAttendances_Attendances_AttendanceId",
                        column: x => x.AttendanceId,
                        principalTable: "Attendances",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_ApplicationUserId",
                table: "Attendances",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_EpisodeId",
                table: "Attendances",
                column: "EpisodeId");

            migrationBuilder.CreateIndex(
                name: "IX_LogAttendances_AttendanceId",
                table: "LogAttendances",
                column: "AttendanceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogAttendances");

            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Students",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
