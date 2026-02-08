using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class DBV12_RemovePrayerTime_ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_PrayerTimes_PrayerTimeId",
                table: "Episodes");

            migrationBuilder.DropIndex(
                name: "IX_Episodes_PrayerTimeId",
                table: "Episodes");

            migrationBuilder.DropColumn(
                name: "PrayerTimeId",
                table: "Episodes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PrayerTimeId",
                table: "Episodes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_PrayerTimeId",
                table: "Episodes",
                column: "PrayerTimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_PrayerTimes_PrayerTimeId",
                table: "Episodes",
                column: "PrayerTimeId",
                principalTable: "PrayerTimes",
                principalColumn: "Id");
        }
    }
}
