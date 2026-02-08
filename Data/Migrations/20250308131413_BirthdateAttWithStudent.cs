using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class BirthdateAttWithStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Roles",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Students",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Students");

            migrationBuilder.AddColumn<string>(
                name: "Roles",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
