using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEntityDemand : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Demands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Request_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Request_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Request_Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Demands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Demands_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Demands_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Demands_ApplicationUserId",
                table: "Demands",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Demands_StudentId",
                table: "Demands",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Demands");
        }
    }
}
