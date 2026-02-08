using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPrayerTimeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AttendType",
                table: "Episodes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<Guid>(
                name: "PrayerTimeId",
                table: "Episodes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "PrayerTimes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    PrayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrayTime = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrayerTimes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_PrayerTimeId",
                table: "Episodes",
                column: "PrayerTimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_PrayerTimes_PrayerTimeId",
                table: "Episodes",
                column: "PrayerTimeId",
                principalTable: "PrayerTimes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_PrayerTimes_PrayerTimeId",
                table: "Episodes");

            migrationBuilder.DropTable(
                name: "PrayerTimes");

            migrationBuilder.DropIndex(
                name: "IX_Episodes_PrayerTimeId",
                table: "Episodes");

            migrationBuilder.DropColumn(
                name: "PrayerTimeId",
                table: "Episodes");

            migrationBuilder.AlterColumn<string>(
                name: "AttendType",
                table: "Episodes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
