using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class StudentDemandsEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Request_Type",
                table: "Demands",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndHoliday",
                table: "Demands",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                table: "Demands",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsOpened",
                table: "Demands",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NewEpisode",
                table: "Demands",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NotificationText",
                table: "Demands",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OldEpisode",
                table: "Demands",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartHoliday",
                table: "Demands",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndHoliday",
                table: "Demands");

            migrationBuilder.DropColumn(
                name: "IsAccepted",
                table: "Demands");

            migrationBuilder.DropColumn(
                name: "IsOpened",
                table: "Demands");

            migrationBuilder.DropColumn(
                name: "NewEpisode",
                table: "Demands");

            migrationBuilder.DropColumn(
                name: "NotificationText",
                table: "Demands");

            migrationBuilder.DropColumn(
                name: "OldEpisode",
                table: "Demands");

            migrationBuilder.DropColumn(
                name: "StartHoliday",
                table: "Demands");

            migrationBuilder.AlterColumn<string>(
                name: "Request_Type",
                table: "Demands",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(80)",
                oldMaxLength: 80,
                oldNullable: true);
        }
    }
}
