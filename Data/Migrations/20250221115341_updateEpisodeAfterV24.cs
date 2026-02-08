using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateEpisodeAfterV24 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeOnly>(
                name: "EndTimeEpisode",
                table: "Episodes",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "ExtraTimeEpisode",
                table: "Episodes",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "StartTimeEpisode",
                table: "Episodes",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTimeEpisode",
                table: "Episodes");

            migrationBuilder.DropColumn(
                name: "ExtraTimeEpisode",
                table: "Episodes");

            migrationBuilder.DropColumn(
                name: "StartTimeEpisode",
                table: "Episodes");
        }
    }
}
