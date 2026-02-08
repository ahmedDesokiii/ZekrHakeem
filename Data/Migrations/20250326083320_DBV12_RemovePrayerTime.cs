using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class DBV12_RemovePrayerTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_PrayerTimes_PrayerTimeId",
                table: "Episodes");

            migrationBuilder.AlterColumn<Guid>(
                name: "PrayerTimeId",
                table: "Episodes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_PrayerTimes_PrayerTimeId",
                table: "Episodes",
                column: "PrayerTimeId",
                principalTable: "PrayerTimes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_PrayerTimes_PrayerTimeId",
                table: "Episodes");

            migrationBuilder.AlterColumn<Guid>(
                name: "PrayerTimeId",
                table: "Episodes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_PrayerTimes_PrayerTimeId",
                table: "Episodes",
                column: "PrayerTimeId",
                principalTable: "PrayerTimes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
