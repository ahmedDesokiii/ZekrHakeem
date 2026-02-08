using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class StudentProgEntityEditing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EpisodeProgId",
                table: "StudentProgs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StEpisodeProgId",
                table: "StudentProgs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentProgs_StEpisodeProgId",
                table: "StudentProgs",
                column: "StEpisodeProgId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentProgs_EpisodeProgs_StEpisodeProgId",
                table: "StudentProgs",
                column: "StEpisodeProgId",
                principalTable: "EpisodeProgs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentProgs_EpisodeProgs_StEpisodeProgId",
                table: "StudentProgs");

            migrationBuilder.DropIndex(
                name: "IX_StudentProgs_StEpisodeProgId",
                table: "StudentProgs");

            migrationBuilder.DropColumn(
                name: "EpisodeProgId",
                table: "StudentProgs");

            migrationBuilder.DropColumn(
                name: "StEpisodeProgId",
                table: "StudentProgs");
        }
    }
}
