using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAllTablesOfZH : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CenterApplicationUsers");

            migrationBuilder.AddColumn<string>(
                name: "Roles",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Roles",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "CenterApplicationUsers",
                columns: table => new
                {
                    CenterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterApplicationUsers", x => new { x.CenterId, x.ApplicationUserId });
                    table.ForeignKey(
                        name: "FK_CenterApplicationUsers_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CenterApplicationUsers_Centers_CenterId",
                        column: x => x.CenterId,
                        principalTable: "Centers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CenterApplicationUsers_ApplicationUserId",
                table: "CenterApplicationUsers",
                column: "ApplicationUserId");
        }
    }
}
