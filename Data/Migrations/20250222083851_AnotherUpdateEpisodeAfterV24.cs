using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class AnotherUpdateEpisodeAfterV24 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtraTimeEpisode",
                table: "Episodes");

            migrationBuilder.AlterColumn<string>(
                name: "StartTimeEpisode",
                table: "Episodes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time");

            migrationBuilder.AlterColumn<string>(
                name: "EndTimeEpisode",
                table: "Episodes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeOnly>(
                name: "StartTimeEpisode",
                table: "Episodes",
                type: "time",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "EndTimeEpisode",
                table: "Episodes",
                type: "time",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "ExtraTimeEpisode",
                table: "Episodes",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }
    }
}
