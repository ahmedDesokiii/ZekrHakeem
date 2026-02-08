using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProgEntity_Kind : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgEpisode_EpisodesTypes_EpisodeTypeId",
                table: "ProgEpisode");

            migrationBuilder.DropIndex(
                name: "IX_ProgEpisode_EpisodeTypeId",
                table: "ProgEpisode");

            migrationBuilder.DropColumn(
                name: "EpisodeTypeId",
                table: "ProgEpisode");

            migrationBuilder.AddColumn<string>(
                name: "Kind",
                table: "Progs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAvilable",
                table: "ProgEpisode",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ManualFinished",
                table: "ProgEpisode",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kind",
                table: "Progs");

            migrationBuilder.DropColumn(
                name: "IsAvilable",
                table: "ProgEpisode");

            migrationBuilder.DropColumn(
                name: "ManualFinished",
                table: "ProgEpisode");

            migrationBuilder.AddColumn<Guid>(
                name: "EpisodeTypeId",
                table: "ProgEpisode",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProgEpisode_EpisodeTypeId",
                table: "ProgEpisode",
                column: "EpisodeTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgEpisode_EpisodesTypes_EpisodeTypeId",
                table: "ProgEpisode",
                column: "EpisodeTypeId",
                principalTable: "EpisodesTypes",
                principalColumn: "Id");
        }
    }
}
