using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateDate_in_Centers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Centers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Centers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedDate",
                value: new DateTime(2026, 5, 8, 8, 7, 50, 425, DateTimeKind.Local).AddTicks(4724));

            migrationBuilder.UpdateData(
                table: "Centers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedDate",
                value: new DateTime(2026, 5, 8, 8, 7, 50, 425, DateTimeKind.Local).AddTicks(4913));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Centers");
        }
    }
}
