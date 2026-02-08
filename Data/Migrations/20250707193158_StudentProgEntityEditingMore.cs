using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class StudentProgEntityEditingMore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentProgEpisodeProgs");

            migrationBuilder.DropColumn(
                name: "EpisodeProgId",
                table: "StudentProgs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EpisodeProgId",
                table: "StudentProgs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StudentProgEpisodeProgs",
                columns: table => new
                {
                    EpisodeProgId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentProgId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentProgEpisodeProgs", x => new { x.EpisodeProgId, x.StudentProgId });
                    table.ForeignKey(
                        name: "FK_StudentProgEpisodeProgs_EpisodeProgs_EpisodeProgId",
                        column: x => x.EpisodeProgId,
                        principalTable: "EpisodeProgs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentProgEpisodeProgs_StudentProgs_StudentProgId",
                        column: x => x.StudentProgId,
                        principalTable: "StudentProgs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentProgEpisodeProgs_StudentProgId",
                table: "StudentProgEpisodeProgs",
                column: "StudentProgId");
        }
    }
}
